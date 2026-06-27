using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class ListPatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== LIST PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
            Example6();
        }

        // Pusta lista
        private static void Example1()
        {
            Console.WriteLine("Example 1 - Empty List");

            int[] numbers = [];

            var message = numbers switch
            {
                [] => "Lista jest pusta",
                _ => "Lista zawiera elementy"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Jeden element
        private static void Example2()
        {
            Console.WriteLine("Example 2 - Single Item");

            int[] numbers = [42];

            var message = numbers switch
            {
                [var value] => $"Jeden element: {value}",
                _ => "Inna liczba elementów"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Dokładna sekwencja
        private static void Example3()
        {
            Console.WriteLine("Example 3 - Exact Sequence");

            int[] numbers = [1, 2, 3];

            var message = numbers switch
            {
                [1, 2, 3] => "Dokładnie 1,2,3",
                _ => "Inna sekwencja"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Początek listy
        private static void Example4()
        {
            Console.WriteLine("Example 4 - Starts With");

            int[] numbers = [10, 20, 30, 40, 50];

            var message = numbers switch
            {
                [10, 20, ..] => "Lista zaczyna się od 10,20",

                [10, ..] => "Lista zaczyna się od 10",

                _ => "Inny początek"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Koniec listy
        private static void Example5()
        {
            Console.WriteLine("Example 5 - Ends With");

            string[] roles =
            [
                "User",
            "Manager",
            "Administrator"
            ];

            var message = roles switch
            {
                [.., "Administrator"]
                    => "Ostatnią rolą jest Administrator",

                [.., "Manager"]
                    => "Ostatnią rolą jest Manager",

                _ => "Inna konfiguracja"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Lista produktów
        private static void Example6()
        {
            Console.WriteLine("Example 6 - Order Items");

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
                    City = "Warszawa"
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
                    Name = "Mysz",
                    Price = 200,
                    Quantity = 1
                },

                new Product
                {
                    Name = "Klawiatura",
                    Price = 400,
                    Quantity = 1
                }
                ],

                Total = 5600
            };

            var result = order.Items switch
            {
                [] =>
                    "Koszyk pusty",

                [var product] =>
                    $"Kupiono tylko {product.Name}",

                [var first, var second] =>
                    $"Dwa produkty: {first.Name}, {second.Name}",

                [var first, .., var last] =>
                    $"Pierwszy: {first.Name}, ostatni: {last.Name}",

                _ =>
                    "Inny przypadek"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
