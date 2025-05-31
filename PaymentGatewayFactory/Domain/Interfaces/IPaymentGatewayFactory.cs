using PaymentGatewayFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Domain.Interfaces
{
    /// <summary>
    /// Fabryka odpowiedzialna za wybór i dostarczenie konkretnej bramki płatności
    /// na podstawie kodu kraju (countryCode).
    /// </summary>
    public interface IPaymentGatewayFactory
    {
        /// <summary>
        /// Zwraca implementację IPaymentGateway odpowiednią dla danego kodu kraju.
        /// </summary>
        /// <param name="countryCode">Kod kraju jako enum CountryCode.</param>
        /// <returns>Instancja IPaymentGateway.</returns>
        IPaymentGateway GetGateway(CountryCode countryCode);
    }
}
