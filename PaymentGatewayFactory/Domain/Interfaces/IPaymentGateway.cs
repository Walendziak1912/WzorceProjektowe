using PaymentGatewayFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Domain.Interfaces
{
    /// <summary>
    /// Interfejs, który każda bramka płatności musi implementować.
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Wykonuje obciążenie karty/rachunku na określoną kwotę.
        /// </summary>
        /// <param name="amount">Kwota do pobrania.</param>
        /// <param name="details">Szczegóły płatności (np. numer karty).</param>
        void Charge(decimal amount, PaymentDetails details);

        /// <summary>
        /// Zwraca środki na wskazane ID transakcji (reimbursement).
        /// </summary>
        /// <param name="transactionId">ID transakcji do zwrotu.</param>
        void Refund(string transactionId);

        /// <summary>
        /// Pobiera status konkretnej transakcji.
        /// </summary>
        /// <param name="transactionId">ID transakcji.</param>
        /// <returns>Status transakcji (np. "Succeeded", "Failed", itp.).</returns>
        string GetTransactionStatus(string transactionId);
    }
}
