using PatternMatchingConsoleDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Examples
{
    public static class TypePatternExamples
    {
        public static void Run()
        {
            Console.WriteLine("===== TYPE PATTERNS =====");
            Console.WriteLine();

            Example1();
            Example2();
            Example3();
            Example4();
        }

        private static void Example1()
        {
            Console.WriteLine("Example 1 - proste sprawdzanie typu");

            object value = "Pattern Matching";

            var result = value switch
            {
                string text => $"To jest string: {text}",
                int number => $"To jest int: {number}",
                decimal amount => $"To jest decimal: {amount}",
                null => "To jest null",
                _ => "Nieznany typ"
            };

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private static void Example2()
        {
            Console.WriteLine("Example 2 - Result pattern");

            Result result = new Success("Operacja zakończona powodzeniem");

            var message = result switch
            {
                Success success => success.Message,
                ValidationError error => $"Błąd walidacji: {error.Error}",
                Unauthorized => "Brak autoryzacji",
                NotFound => "Nie znaleziono zasobu",
                _ => "Nieznany wynik"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void Example3()
        {
            Console.WriteLine("Example 3 - typ + właściwości");

            Result result = new ValidationError("Email jest wymagany");

            var message = result switch
            {
                Success { Message: "OK" }
                    => "Szybka ścieżka sukcesu",

                Success success
                    => $"Sukces: {success.Message}",

                ValidationError { Error: not null } error
                    => $"Błąd walidacji: {error.Error}",

                Unauthorized
                    => "Użytkownik nie ma dostępu",

                NotFound
                    => "Obiekt nie istnieje",

                _ => "Nieobsłużony przypadek"
            };

            Console.WriteLine(message);
            Console.WriteLine();
        }

        private static void Example4()
        {
            Console.WriteLine("Example 4 - eventy biznesowe");

            var events = new DomainEvent[]
            {
            new UserCreatedEvent("Anna"),
            new UserDeletedEvent("Jan"),
            new OrderPaidEvent(123, 499.99m),
            new PasswordChangedEvent("Tomasz")
            };

            foreach (var domainEvent in events)
            {
                var action = domainEvent switch
                {
                    UserCreatedEvent e
                        => $"Wyślij mail powitalny do: {e.UserName}",

                    UserDeletedEvent e
                        => $"Usuń dane użytkownika: {e.UserName}",

                    OrderPaidEvent e
                        => $"Wystaw fakturę dla zamówienia #{e.OrderId}, kwota: {e.Amount:C}",

                    PasswordChangedEvent e
                        => $"Wyślij alert bezpieczeństwa do: {e.UserName}",

                    _ => "Nieznane zdarzenie"
                };

                Console.WriteLine(action);
            }

            Console.WriteLine();
        }
    }
    public abstract record DomainEvent;
    public sealed record UserCreatedEvent(string UserName) : DomainEvent;
    public sealed record UserDeletedEvent(string UserName) : DomainEvent;
    public sealed record OrderPaidEvent(int OrderId, decimal Amount) : DomainEvent;
    public sealed record PasswordChangedEvent(string UserName) : DomainEvent;
}
