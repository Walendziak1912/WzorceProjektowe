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
    /// Przykładowa implementacja bramki płatności PayU.
    /// </summary>
    public class PayUGateway : IPaymentGateway
    {
        // Załóżmy, że mamy jakiś klient API do PayU.
        public PayUGateway()
        {
            // ewentualna inicjalizacja klienta
        }

        public void Charge(decimal amount, PaymentDetails details)
        {
            // Pseudokod: wywołanie API PayU
            Console.WriteLine($"[PayU] Charge: {amount} {details.CardNumber}");
        }

        public void Refund(string transactionId)
        {
            // Pseudokod do zwrotu środków w PayU
            Console.WriteLine($"[PayU] Refund transaction: {transactionId}");
        }

        public string GetTransactionStatus(string transactionId)
        {
            // Pseudokod: pobranie statusu z PayU API
            var status = "COMPLETED"; // na potrzeby przykładu zwracamy na sztywno
            Console.WriteLine($"[PayU] Transaction status for {transactionId}: {status}");
            return status;
        }
    }
}
