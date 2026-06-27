using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    //To będzie chyba najbardziej "produkcyjny" plik.Większość programistów używa switch, ale często nie wykorzystuje go jako wyrażenia(switch expression), które zwraca wartość.
    //W nowoczesnym C# bardzo często zastępuje on długie if/else i klasyczne instrukcje switch.
    public static class SwitchExpressionExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== SWITCH EXPRESSIONS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
            Example6();
        }

        // Enum -> tekst
        private static void Example1()
        {
            Console.WriteLine("Example 1 - Enum");

            var status = OrderStatus.Paid;

            var description = status switch
            {
                OrderStatus.Draft => "Robocze",
                OrderStatus.Paid => "Opłacone",
                OrderStatus.Shipped => "Wysłane",
                OrderStatus.Cancelled => "Anulowane",
                _ => "Nieznany status"
            };

            Console.WriteLine(description);
            Console.WriteLine();
        }

        // HTTP Status
        private static void Example2()
        {
            Console.WriteLine("Example 2 - HTTP");

            var statusCode = 404;

            var message = statusCode switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        // Mapowanie Result
        private static void Example3()
        {
            Console.WriteLine("Example 3 - Result");

            Result result = new ValidationError("Email jest wymagany");

            var response = result switch
            {
                Success success =>
                    $"200 OK -> {success.Message}",

                ValidationError validation =>
                    $"400 Bad Request -> {validation.Error}",

                Unauthorized =>
                    "401 Unauthorized",

                NotFound =>
                    "404 Not Found",

                _ =>
                    "500 Internal Server Error"
            };

            Console.WriteLine(response);
            Console.WriteLine();
        }

        // Klasyfikacja zamówienia
        private static void Example4()
        {
            Console.WriteLine("Example 4 - Order classification");

            var order = CreateOrder();

            var category = order switch
            {
                {
                    Total: > 10000,
                    Customer: { IsVip: true }
                }
                    => "Strategiczne",

                {
                    Total: > 5000
                }
                    => "Premium",

                {
                    Total: > 1000
                }
                    => "Standard",

                _ => "Małe"
            };

            Console.WriteLine(category);
            Console.WriteLine();
        }

        // Pogoda
        private static void Example5()
        {
            Console.WriteLine("Example 5 - Weather");

            var report = new WeatherReport
            {
                Temperature = 28,
                IsRaining = false,
                WindSpeed = 8
            };

            var recommendation = report switch
            {
                { Temperature: > 30 }
                    => "Zostań w domu",

                { IsRaining: true }
                    => "Weź parasol",

                { Temperature: >= 20 }
                    => "Idealny dzień na spacer",

                _ =>
                    "Ubierz kurtkę"
            };

            Console.WriteLine(recommendation);
            Console.WriteLine();
        }

        // Kalkulator
        private static void Example6()
        {
            Console.WriteLine("Example 6 - Calculator");

            Console.WriteLine(Calculate(20, 10, '+'));
            Console.WriteLine(Calculate(20, 10, '-'));
            Console.WriteLine(Calculate(20, 10, '*'));
            Console.WriteLine(Calculate(20, 10, '/'));
            Console.WriteLine();
        }

        private static decimal Calculate(decimal left, decimal right, char operation)
        {
            return operation switch
            {
                '+' => left + right,
                '-' => left - right,
                '*' => left * right,
                '/' when right != 0 => left / right, //warto pokazać, że switch expression można wzbogacać o dodatkowe warunki (when),
                '/' => throw new DivideByZeroException(),
                _ => throw new InvalidOperationException("Nieznany operator.")
            };
        }

        private static Order CreateOrder()
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
                    Amount = 7500
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
                    Price = 7500,
                    Quantity = 1
                }
                ],

                Total = 7500
            };
        }
    }
}
