using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 2);
            warehouse.Delive(iPhone12, 10);

            warehouse.ShowCells();

            Console.WriteLine();

            Cart cart = shop.TakeCart();

            cart.Add(iPhone11, 2);
            cart.Add(iPhone12, 5);
            cart.Add(iPhone12, 2);

            cart.Show();

            Console.WriteLine();

            warehouse.ShowCells();

            Console.WriteLine();

            cart.Cancel();

            cart.Show();

            Console.WriteLine();

            warehouse.ShowCells();

            Console.WriteLine("\n" + cart.Order());
        }
    }

    class Good
    {
        public string Name { get; private set; }

        public Good(string name)
        {
            if (name.Length <= 0)
                throw new ArgumentException("Некорректное название товара");

            Name = name;
        }
    }

    class Cell
    {
        public Good Good { get; private set; }
        public int Count { get; private set; }

        public Cell(Good good, int count)
        {
            if (count <= 0)
                throw new ArgumentException("Некорректное количество товара");

            Good = good;
            Count = count;
        }

        public void Take(int count)
        {
            if (Count < count)
                throw new ArgumentException("Некорректное количество товара");

            Count -= count;
        }

        public void Merge(Cell newCell)
        {
            if (newCell.Good != Good)
                throw new InvalidOperationException();

            Count += newCell.Count;
        }
    }

    class Warehouse
    {
        private List<Cell> _cells = new List<Cell>();

        public void Delive(Good good, int count)
        {
            var newCell = new Cell(good, count);

            Cell cell = _cells.FirstOrDefault(c => c.Good == good);

            if (cell == null)
                _cells.Add(newCell);
            else
                cell.Merge(newCell);
        }

        public void ShowCells()
        {
            foreach (var cell in _cells)
            {
                Console.WriteLine($"Товар - {cell.Good.Name}, Осталось на складе - {cell.Count} шт.");
            }
        }

        public Cell TakeGood(Good good, int count)
        {
            var newCell = _cells.FirstOrDefault(c => c.Good == good);

            if (newCell == null)
            {
                Console.WriteLine("Такого товара нет на складе.");
                return null;
            }

            if (newCell.Count < count)
            {
                Console.WriteLine("Недостаточно товара на складе.");
                return null;
            }

            newCell.Take(count);

            return new Cell(newCell.Good, count);
        }
    }

    class Shop
    {
        private Warehouse _warehouse;

        public string Paylink { get; private set; } = "Случайная строка";

        public Shop(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new InvalidOperationException();

            _warehouse = warehouse;
        }

        public Cell Buy(Good good, int count)
        {
            return _warehouse.TakeGood(good, count);
        }

        public Cart TakeCart() => new Cart(this);

        public void ReturnToWarehouse(IReadOnlyList<Cell> cells)
        {
            foreach (var cell in cells)
            {
                _warehouse.Delive(cell.Good, cell.Count);
            }
        }
    }

    class Cart
    {
        private Shop _shop;
        private List<Cell> _buyedGoods = new List<Cell>();

        public Cart(Shop shop)
        {
            if (shop == null)
                throw new InvalidOperationException();

            _shop = shop;
        }

        public void Add(Good good, int count)
        {
            Cell newCell = _shop.Buy(good, count);

            Cell cell = _buyedGoods.FirstOrDefault(c => c.Good == good);

            if (newCell == null)
                return;

            if (cell == null)
                _buyedGoods.Add(newCell);
            else
                cell.Merge(newCell);
        }

        public void Show()
        {
            Console.WriteLine("Товары в корзине: ");

            foreach (var cell in _buyedGoods)
            {
                Console.WriteLine($"Товар - {cell.Good.Name}, количество - {cell.Count} шт.");
            }
        }

        public void Pay()
        {
            _buyedGoods.Clear();
        }

        public void Cancel()
        {
            _shop.ReturnToWarehouse(_buyedGoods);

            _buyedGoods.Clear();
        }

        public string Order() => _shop.Paylink;
    }
}