using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayFactory.Application.Services;
using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using PaymentGatewayFactory.Infrastructure.Extensions;
using Factories = PaymentGatewayFactory.Application.Factories;
using static PaymentGatewayFactory.Domain.Models.CountryCode;
namespace PaymentGatewayFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Konfiguracja kontenera DI
            var serviceCollection = new ServiceCollection();

            // 1.1. Rejestracja bramek płatności (Infrastructure)
            //     Każda klasa bramki implementuje IPaymentGateway
            serviceCollection.AddPaymentGateways();

            // 1.2. Rejestracja fabryki (Application)
            //      Fabryka zwraca IPaymentGateway zależnie od kodu kraju
            serviceCollection.AddSingleton<IPaymentGatewayFactory, Factories.PaymentGatewayFactory>();

            // 1.3. Rejestracja serwisu płatności (Application)
            //      Serwis będzie pobierał odpowiednią bramkę przez fabrykę
            serviceCollection.AddTransient<PaymentService>();

            // 1.4. Budowanie ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // 2. Uzyskanie instancji serwisu z kontenera
            var paymentService = serviceProvider.GetRequiredService<PaymentService>();

            // 3. Przykładowe wywołania: symulujemy płatność, zwrot i pobranie statusu

            // 3.1. Przykład 1: Wykonanie płatności w USA (CountryCode.US) przez PayPal
            Console.WriteLine(">>> Próba przetworzenia płatności w USA przez PayPal:");
            var paymentDetailsUS = new PaymentDetails
            {
                CardNumber = "4111111111111111",
                CardHolderName = "Jan Kowalski",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            decimal amountUS = 150.00m;
            CountryCode countryCodeUS = US;

            try
            {
                paymentService.ProcessPayment(amountUS, countryCodeUS, paymentDetailsUS);
                Console.WriteLine("Płatność w USA przez PayPal zakończona pomyślnie.\n");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd podczas płatności: {ex.Message}\n");
            }

            // 3.2. Przykład 2: Wykonanie płatności w Polsce (CountryCode.PL) przez PayU
            Console.WriteLine(">>> Próba przetworzenia płatności w Polsce przez PayU:");
            var paymentDetailsPL = new PaymentDetails
            {
                CardNumber = "5500000000000004",
                CardHolderName = "Anna Nowak",
                ExpiryMonth = "06",
                ExpiryYear = "2026",
                Cvv = "456"
            };
            decimal amountPL = 200.00m;
            CountryCode countryCodePL = PL;

            try
            {
                paymentService.ProcessPayment(amountPL, countryCodePL, paymentDetailsPL);
                Console.WriteLine("Płatność w Polsce przez PayU zakończona pomyślnie.\n");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd podczas płatności: {ex.Message}\n");
            }

            // 3.3. Przykład 3: Zwrot (refund) płatności w USA przez PayPal
            Console.WriteLine(">>> Próba zwrotu płatności w USA przez PayPal:");
            string transactionIdUS = "tx123_us_abc";
            try
            {
                paymentService.RefundPayment(countryCodeUS, transactionIdUS);
                Console.WriteLine("Zwrot w USA przez PayPal zakończony pomyślnie.\n");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd podczas zwrotu: {ex.Message}\n");
            }

            // 3.4. Przykład 4: Pobranie statusu transakcji w Polsce przez PayU
            Console.WriteLine(">>> Pobieranie statusu transakcji w Polsce przez PayU:");
            string transactionIdPL = "tx789_pl_xyz";
            try
            {
                var status = paymentService.GetStatus(countryCodePL, transactionIdPL);
                Console.WriteLine($"Status transakcji {transactionIdPL} (PL): {status}\n");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieoczekiwany błąd podczas pobierania statusu: {ex.Message}\n");
            }

            // 3.5. Przykład 5: Próbujemy użyć nieobsługiwanego kodu kraju
            // Uwaga: W przypadku enuma nie możemy już użyć nieistniejącego kodu kraju,
            // ponieważ kompilator wyłapie błąd. Zamiast tego możemy zasymulować błąd
            // używając wartości spoza zakresu enuma poprzez rzutowanie.
            Console.WriteLine(">>> Próba użycia nieznanego kodu kraju (wartość spoza enuma):");
            try
            {
                // Rzutujemy liczbę 999 na enum CountryCode, co da nam wartość spoza zakresu enuma
                paymentService.ProcessPayment(50.00m, (CountryCode)999, paymentDetailsUS);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Oczekiwany błąd (nieznany kod kraju): {ex.Message}\n");
            }

            // 4. Koniec programu
            Console.WriteLine("Koniec działania aplikacji. Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}
