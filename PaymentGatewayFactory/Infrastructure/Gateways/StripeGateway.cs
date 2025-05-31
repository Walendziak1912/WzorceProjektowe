using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Infrastructure.Gateways
{
    /// <summary>
    /// Przykładowa implementacja bramki płatności Stripe.
    /// </summary>
    public class StripeGateway : IPaymentGateway
    {
        // Załóżmy, że mamy jakiś klient API do Stripe’a.
        // W praktyce tutaj wstrzykujesz StripeClient lub Settings przez konstruktor.
        public StripeGateway()
        {
            // ewentualna inicjalizacja klienta
        }

        public void Charge(decimal amount, PaymentDetails details)
        {
            // Poniżej pseudokod: wywołanie API Stripe’a
            // StripeClient.Charge(amount, details.CardNumber, ...);
            Console.WriteLine($"[Stripe] Charge: {amount} {details.CardNumber}");
        }

        public void Refund(string transactionId)
        {
            // Pseudokod do zwrotu środków w Stripe
            // StripeClient.Refund(transactionId);
            Console.WriteLine($"[Stripe] Refund transaction: {transactionId}");
        }

        public string GetTransactionStatus(string transactionId)
        {
            // Pseudokod: pobranie statusu z Stripe API
            // var status = StripeClient.GetStatus(transactionId);
            var status = "Succeeded"; // na potrzeby przykładu zwracamy na sztywno
            Console.WriteLine($"[Stripe] Transaction status for {transactionId}: {status}");
            return status;
        }
    }
}
