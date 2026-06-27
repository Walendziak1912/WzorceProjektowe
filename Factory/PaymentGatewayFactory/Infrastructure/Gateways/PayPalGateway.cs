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
    /// Przykładowa implementacja bramki płatności PayPal.
    /// </summary>
    public class PayPalGateway : IPaymentGateway
    {
        // Załóżmy, że mamy jakiś klient API do PayPal.
        public PayPalGateway()
        {
            // ewentualna inicjalizacja klienta
        }

        public void Charge(decimal amount, PaymentDetails details)
        {
            // Pseudokod: wywołanie API PayPal
            Console.WriteLine($"[PayPal] Charge: {amount} {details.CardNumber}");
        }

        public void Refund(string transactionId)
        {
            // Pseudokod do zwrotu środków w PayPal
            Console.WriteLine($"[PayPal] Refund transaction: {transactionId}");
        }

        public string GetTransactionStatus(string transactionId)
        {
            // Pseudokod: pobranie statusu z PayPal API
            var status = "SUCCESS"; // na potrzeby przykładu zwracamy na sztywno
            Console.WriteLine($"[PayPal] Transaction status for {transactionId}: {status}");
            return status;
        }
    }
}
