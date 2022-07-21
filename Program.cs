using System;
using System.Security.Cryptography;
using System.Text;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order(100, 100);

            MasterCard masterCard = new MasterCard();
            Visa visa = new Visa();
            GooglePay googlePay = new GooglePay();

            Console.WriteLine(masterCard.GetPayingLink(order) + "\n");
            Console.WriteLine(visa.GetPayingLink(order) + "\n");
            Console.WriteLine(googlePay.GetPayingLink(order) + "\n");
        }
    }

    class Order 
    {
        public int Id { get; private set; }
        public int Amount { get; private set; }

        public Order(int id, int amount)
        {
            if (id < 0)
                throw new ArgumentException(nameof(id));

            if (amount < 0)
                throw new ArgumentException(nameof(amount));

            Id = id;
            Amount = amount;
        }
    }

    interface IPaymentSystem
    {
        public string URLLink { get; }

        public string GetPayingLink(Order order);
    }

    class MasterCard : IPaymentSystem
    {
        public string URLLink => "pay.system1.ru/order?amount=12000RUB&hash=";

        public string GetPayingLink(Order order)
        {
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(order.Id.ToString()));

            return URLLink + Convert.ToBase64String(hash);
        }
    }

    class Visa : IPaymentSystem
    {
        public string URLLink => "order.system2.ru/pay?hash=";

        public string GetPayingLink(Order order)
        {
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes((order.Id + order.Amount).ToString()));

            return URLLink + Convert.ToBase64String(hash);
        }
    }

    class GooglePay : IPaymentSystem
    {
        private string _salt = "ijunior";

        public string URLLink => "system3.com/pay?amount=12000&curency=RUB&hash=";

        public string GetPayingLink(Order order)
        {
            var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes((order.Amount + order.Id + _salt).ToString()));

            return URLLink + Convert.ToBase64String(hash);
        }
    }
}