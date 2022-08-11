using System;
using System.Collections.Generic;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var paymentSystems = new List<PaymentSystem>();

            paymentSystems.Add(new Qiwi("Qiwi"));
            paymentSystems.Add(new WebMoney("WebMoney"));
            paymentSystems.Add(new Card("Card"));

            PaymentSystem paymentSystem;

            var orderForm = new OrderForm();

            paymentSystem = orderForm.ShowForm(paymentSystems);

            if (paymentSystem == null)
                return;

            paymentSystem.Pay();
            paymentSystem.ShowPaymentResult();
        }
    }

    public abstract class PaymentSystem
    {
        public PaymentSystem(string label)
        {
            Label = label;
        }

        public string Label { get; protected set; }

        public abstract void Pay();
        public abstract void ShowPaymentResult();
    }

    public class Qiwi : PaymentSystem
    {
        public Qiwi(string label) : base (label)
        {
            Label = label;
        }

        public override void Pay()
        {
            Console.WriteLine($"Переход на страницу {Label}...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine($"Проверка платежа {Label}...\nОперация прошла успешно.");
        }
    }

    public class WebMoney : PaymentSystem
    {
        public WebMoney(string label) : base(label)
        {
            Label = label;
        }

        public override void Pay()
        {
            Console.WriteLine($"Переход на страницу {Label}...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine($"Проверка платежа {Label}...\nОперация прошла успешно.");
        }
    }

    public class Card : PaymentSystem
    {
        public Card(string label) : base(label)
        {
            Label = label;
        }

        public override void Pay()
        {
            Console.WriteLine("Переход на страницу оплаты...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine("Проверка платежа...\nОперация прошла успешно.");
        }
    }

    public class OrderForm
    {
        public PaymentSystem ShowForm(List<PaymentSystem> paymentSystems)
        {
            Console.Write("Мы принимаем: ");

            foreach (var paymentSystem in paymentSystems)
            {
                Console.Write( paymentSystem.Label + " ");
            }

            Console.WriteLine("\nКакой системой вы хотите совершить оплату?");

            string userInput = Console.ReadLine();

            foreach (var paymentSystem in paymentSystems)
            {
                if (paymentSystem.Label == userInput)
                {
                    return paymentSystem;
                }
            }

            Console.WriteLine("Такой платежной системы нет.");
            Console.ReadKey();
            
            return null;
        }
    }
}