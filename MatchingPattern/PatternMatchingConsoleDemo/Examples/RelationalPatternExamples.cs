using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class RelationalPatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== RELATIONAL PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
        }

        private static void Example1()
        {
            Console.WriteLine("Example 1 temperatura");

            var report = new WeatherReport
            {
                Temperature = 31,
                IsRaining = false,
                WindSpeed = 12
            };

            var message = report.Temperature switch
            {
                < 0 => "Mróz",
                >= 0 and < 10 => "Zimno",
                >= 10 and < 25 => "Przyjemnie",
                >= 25 and < 35 => "Gorąco",
                >= 35 => "Upał ekstremalny"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void Example2()
        {
            Console.WriteLine("Example 2 wartość zamówienia");

            var order = CreateOrder(total: 7_500);

            var category = order.Total switch
            {
                < 100 => "Małe zamówienie",
                >= 100 and < 1_000 => "Standardowe zamówienie",
                >= 1_000 and < 5_000 => "Duże zamówienie",
                >= 5_000 => "Zamówienie premium"
            };

            Console.WriteLine(category);
            Console.WriteLine();
        }

        private static void Example3()
        {
            Console.WriteLine("Example 3 liczba ponowień płatności");

            var payment = new Payment
            {
                Status = PaymentStatus.Pending,
                Amount = 250,
                RetryCount = 4
            };

            var action = payment.RetryCount switch
            {
                0 => "Pierwsza próba płatności",
                > 0 and <= 3 => "Ponów płatność",
                > 3 => "Anuluj płatność i poinformuj klienta"
            };

            Console.WriteLine(action);
            Console.WriteLine();
        }

        private static void Example4()
        {
            Console.WriteLine("Example 4 relational pattern wewnątrz property pattern");

            var order = CreateOrder(total: 12_000);

            var decision = order switch
            {
                { Total: > 10_000, Customer: { IsVip: true } }
                    => "Obsłuż jako strategiczne zamówienie VIP",

                { Total: > 10_000 }
                    => "Wymagana ręczna akceptacja",

                { Total: <= 10_000 }
                    => "Standardowa obsługa",

                _ => "Nieznany przypadek"
            };

            Console.WriteLine(decision);
            Console.WriteLine();
        }

        private static Order CreateOrder(decimal total)
        {
            return new Order
            {
                Status = OrderStatus.Paid,

                Customer = new Customer
                {
                    FirstName = "Anna",
                    LastName = "Kowalska",
                    IsVip = true,
                    Country = "PL"
                },

                Payment = new Payment
                {
                    Status = PaymentStatus.Completed,
                    Amount = total,
                    RetryCount = 0
                },

                Shipment = new Shipment
                {
                    Country = "PL",
                    City = "Warszawa",
                    Express = true
                },

                Items =
                [
                    new Product
                {
                    Name = "Laptop",
                    Price = total,
                    Quantity = 1
                }
                ],

                Total = total,
                IsOverdue = false
            };
        }
    }
}
