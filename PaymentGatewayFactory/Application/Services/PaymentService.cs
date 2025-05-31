using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Application.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za operacje płatności w aplikacji.
    /// To tutaj używamy fabryki, aby pobrać właściwą bramkę i wykonać płatność.
    /// </summary>
    public class PaymentService
    {
        private readonly IPaymentGatewayFactory _gatewayFactory;

        public PaymentService(IPaymentGatewayFactory gatewayFactory)
        {
            _gatewayFactory = gatewayFactory;
        }

        /// <summary>
        /// Metoda przetwarzająca płatność dla podanej kwoty i kraju.
        /// </summary>
        public void ProcessPayment(decimal amount, CountryCode countryCode, PaymentDetails details)
        {
            // Pobieramy właściwą bramkę płatności na podstawie countryCode:
            var gateway = _gatewayFactory.GetGateway(countryCode);

            // Wykonujemy operację obciążenia:
            gateway.Charge(amount, details);
        }

        /// <summary>
        /// Metoda zwracająca środki (refund) dla danej transakcji.
        /// </summary>
        public void RefundPayment(CountryCode countryCode, string transactionId)
        {
            var gateway = _gatewayFactory.GetGateway(countryCode);
            gateway.Refund(transactionId);
        }

        /// <summary>
        /// Pobranie statusu transakcji.
        /// </summary>
        public string GetStatus(CountryCode countryCode, string transactionId)
        {
            var gateway = _gatewayFactory.GetGateway(countryCode);
            return gateway.GetTransactionStatus(transactionId);
        }
    }
}
