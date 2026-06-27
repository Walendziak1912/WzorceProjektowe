using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class LogicalPatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== LOGICAL PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
        }

        // OR
        private static void Example1()
        {
            Console.WriteLine("Example 1 - OR");

            var status = OrderStatus.Cancelled;

            var message = status switch
            {
                OrderStatus.Draft or OrderStatus.Cancelled
                    => "Zamówienie nie jest aktywne",

                OrderStatus.Paid or OrderStatus.Shipped
                    => "Zamówienie jest realizowane",

                _ => "Nieznany status"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // NOT
        private static void Example2()
        {
            Console.WriteLine("Example 2 - NOT");

            var user = new User
            {
                Name = "Jan",
                Role = UserRole.Employee,
                IsActive = false
            };

            var result = user switch
            {
                { IsActive: not true }
                    => "Konto jest nieaktywne",

                _ => "Konto aktywne"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        // AND
        private static void Example3()
        {
            Console.WriteLine("Example 3 - AND");

            var temperature = 23;

            var description = temperature switch
            {
                >= 18 and <= 25
                    => "Idealna temperatura",

                > 25
                    => "Ciepło",

                _ => "Zimno"
            };

            Console.WriteLine(description);
            Console.WriteLine();
        }

        // OR + relational pattern
        private static void Example4()
        {
            Console.WriteLine("Example 4 - OR + Relational");

            var age = 70;

            var discount = age switch
            {
                < 18 or >= 65
                    => "Przysługuje zniżka",

                _ => "Cena standardowa"
            };

            Console.WriteLine(discount);
            Console.WriteLine();
        }

        // Łączenie property pattern z logical pattern
        private static void Example5()
        {
            Console.WriteLine("Example 5 - Property + Logical");

            var order = new Order
            {
                Status = OrderStatus.Paid,

                Customer = new Customer
                {
                    FirstName = "Anna",
                    LastName = "Nowak",
                    IsVip = false
                },

                Payment = new Payment
                {
                    Status = PaymentStatus.Completed
                },

                Shipment = new Shipment
                {
                    Country = "DE",
                    City = "Berlin"
                },

                Items =
                [
                    new Product
                {
                    Name = "Laptop",
                    Price = 6000,
                    Quantity = 1
                }
                ],

                Total = 6000
            };

            var action = order switch
            {
                {
                    Status: OrderStatus.Paid or OrderStatus.Shipped,
                    Shipment:
                    {
                        Country: not "PL"
                    }
                }
                    => "Przygotuj dokumenty eksportowe",

                _ => "Standardowa obsługa"
            };

            Console.WriteLine(action);
            Console.WriteLine();
        }
    }
}
