using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class PropertyPatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== PROPERTY PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
        }

        // Dopasowanie pojedynczej właściwości
        private static void Example1()
        {
            Console.WriteLine("Example 1");

            var user = new User
            {
                Name = "Jan",
                IsAdmin = true,
                IsActive = true,
                Role = UserRole.Administrator
            };

            var message = user switch
            {
                { IsAdmin: true } => "Administrator",
                _ => "Normalny użytkownik"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Dopasowanie wielu właściwości jednocześnie
        private static void Example2()
        {
            Console.WriteLine("Example 2");

            var user = new User
            {
                Name = "Anna",
                Role = UserRole.Manager,
                Department = "IT",
                IsActive = true
            };

            var access = user switch
            {
                { Role: UserRole.Manager, Department: "IT" }
                    => "Pełny dostęp do systemów IT",

                { Role: UserRole.Manager }
                    => "Dostęp menedżera",

                _ => "Standardowy dostęp"
            };

            Console.WriteLine(access);
            Console.WriteLine();
        }

        // Zagnieżdżone właściwości
        private static void Example3()
        {
            Console.WriteLine("Example 3");

            var order = new Order
            {
                Status = OrderStatus.Paid,
                Customer = new Customer
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    IsVip = true
                },
                Payment = new Payment
                {
                    Status = PaymentStatus.Completed,
                    Amount = 3500
                },
                Shipment = new Shipment
                {
                    Country = "PL",
                    City = "Warszawa"
                },
                Items =
                [
                    new Product
                {
                    Name = "Laptop",
                    Price = 3500,
                    Quantity = 1
                }
                ],
                Total = 3500
            };

            var result = order switch
            {
                {
                    Customer: { IsVip: true },
                    Payment: { Status: PaymentStatus.Completed }
                }
                    => "VIP z opłaconym zamówieniem",

                _ => "Zwykłe zamówienie"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        //Dopasowanie liczby elementów kolekcji.
        private static void Example4()
        {
            Console.WriteLine("Example 4");

            var order = new Order
            {
                Status = OrderStatus.Paid,

                Customer = new Customer
                {
                    FirstName = "Jan",
                    LastName = "Nowak"
                },

                Payment = new Payment
                {
                    Status = PaymentStatus.Completed
                },

                Shipment = new Shipment
                {
                    Country = "PL",
                    City = "Kraków"
                },

                Items =
                [
                    new Product
                {
                    Name = "Monitor",
                    Price = 1200,
                    Quantity = 1
                },

                new Product
                {
                    Name = "Klawiatura",
                    Price = 400,
                    Quantity = 1
                },

                new Product
                {
                    Name = "Mysz",
                    Price = 250,
                    Quantity = 1
                }
                ],

                Total = 1850
            };

            var message = order switch
            {
                { Items: { Count: > 2 } }
                    => "Duże zamówienie",

                { Items: { Count: 1 } }
                    => "Jedna pozycja",

                _ => "Standardowe zamówienie"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
