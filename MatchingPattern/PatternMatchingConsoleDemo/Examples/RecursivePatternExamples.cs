using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class RecursivePatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== RECURSIVE PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
        }

        // Zagnieżdżone właściwości
        private static void Example1()
        {
            Console.WriteLine("Example 1 - Nested properties");

            var order = CreateVipOrder();

            var result = order switch
            {
                {
                    Customer:
                    {
                        IsVip: true,
                        Country: "PL"
                    },

                    Payment:
                    {
                        Status: PaymentStatus.Completed
                    }
                }
                    => "VIP z opłaconym zamówieniem",

                _ => "Standardowe zamówienie"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        // Relational + Property Pattern
        private static void Example2()
        {
            Console.WriteLine("Example 2 - Nested relational");

            var order = CreateVipOrder();

            var result = order switch
            {
                {
                    Total: > 5000,

                    Customer:
                    {
                        IsVip: true
                    },

                    Payment:
                    {
                        Amount: > 5000
                    }
                }
                    => "Strategiczny klient",

                _ => "Standard"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        // Property + List Pattern
        private static void Example3()
        {
            Console.WriteLine("Example 3 - Nested list pattern");

            var order = CreateVipOrder();

            var result = order switch
            {
                {
                    Items:
                    [
                    { Price: > 3000 },
                        ..
                    ]
                }
                    => "Pierwszy produkt jest drogi",

                _ => "Brak drogich produktów na początku"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        // List Pattern + Property Pattern
        private static void Example4()
        {
            Console.WriteLine("Example 4 - Complex list");

            var order = CreateVipOrder();

            var result = order switch
            {
                {
                    Items:
                    [
                    { Name: "Laptop" },
                    { Name: "Monitor" },
                        ..
                    ]
                }
                    => "Laptop + Monitor",

                _ => "Inna konfiguracja"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        // Wszystkie wzorce naraz
        private static void Example5()
        {
            // W starym stylu oznaczałoby to około 12–15 zagnieżdżonych if-ów z wieloma sprawdzeniami null i odwołaniami do właściwości.
            // Tutaj cała reguła biznesowa jest opisana deklaratywnie jako jeden wzorzec, co świetnie pokazuje, dlaczego pattern matching jest tak ceniony
            Console.WriteLine("Example 5 - Everything together");

            var order = CreateVipOrder();

            var decision = order switch
            {
                {
                    Status: OrderStatus.Paid,

                    Total: > 5000,

                    Customer:
                    {
                        IsVip: true,
                        Country: not "US"
                    },

                    Payment:
                    {
                        Status: PaymentStatus.Completed,
                        RetryCount: <= 1
                    },

                    Shipment:
                    {
                        Country: "PL" or "DE"
                    },

                    Items:
                    [
                    { Price: > 3000 },
                        ..,
                    { Quantity: >= 1 }
                    ]
                }
                    => "Automatyczna realizacja",

                _ =>
                    "Wymagana ręczna weryfikacja"
            };

            Console.WriteLine(decision);
            Console.WriteLine();
        }

        private static Order CreateVipOrder()
        {
            return new Order
            {
                Status = OrderStatus.Paid,

                Customer = new Customer
                {
                    FirstName = "Jan",
                    LastName = "Nowak",
                    IsVip = true,
                    Country = "PL"
                },

                Payment = new Payment
                {
                    Status = PaymentStatus.Completed,
                    Amount = 6200,
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
                    Price = 5000,
                    Quantity = 1
                },

                new Product
                {
                    Name = "Monitor",
                    Price = 900,
                    Quantity = 1
                },

                new Product
                {
                    Name = "Mysz",
                    Price = 300,
                    Quantity = 1
                }
                ],

                Total = 6200
            };
        }
    }
}
