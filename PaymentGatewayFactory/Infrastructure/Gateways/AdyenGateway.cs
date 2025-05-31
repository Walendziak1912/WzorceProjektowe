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
    /// Przykładowa implementacja bramki płatności Adyen.
    /// </summary>
    public class AdyenGateway : IPaymentGateway
    {
        // Załóżmy, że mamy jakiś klient API do Adyen’a.
        public AdyenGateway()
        {
            // ewentualna inicjalizacja klienta
        }

        public void Charge(decimal amount, PaymentDetails details)
        {
            // Pseudokod: wywołanie API Adyen
            Console.WriteLine($"[Adyen] Charge: {amount} {details.CardNumber}");
        }

        public void Refund(string transactionId)
        {
            // Pseudokod do zwrotu środków w Adyen
            Console.WriteLine($"[Adyen] Refund transaction: {transactionId}");
        }

        public string GetTransactionStatus(string transactionId)
        {
            // Pseudokod: pobranie statusu z Adyen API
            var status = "Authorised"; // na potrzeby przykładu zwracamy na sztywno
            Console.WriteLine($"[Adyen] Transaction status for {transactionId}: {status}");
            return status;
        }
    }
}
