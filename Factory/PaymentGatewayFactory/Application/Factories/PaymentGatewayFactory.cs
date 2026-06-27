using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Application.Factories
{
    /// <summary>
    /// Implementacja fabryki bramek płatności.
    /// Na podstawie kodu kraju (countryCode) zwraca odpowiedni obiekt IPaymentGateway.
    /// </summary>
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// W konstruktorze otrzymujemy IServiceProvider (kontener DI),
        /// dzięki czemu możemy w locie pobierać zarejestrowane usługi.
        /// </summary>
        public PaymentGatewayFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IPaymentGateway GetGateway(CountryCode countryCode)
        {
            // Używamy switch expression z C#8+:
            return countryCode switch
            {
                CountryCode.PL => _provider.GetRequiredService<Infrastructure.Gateways.PayUGateway>(),
                CountryCode.US => _provider.GetRequiredService<Infrastructure.Gateways.PayPalGateway>(),
                // Można dodać kolejne przypadki w przyszłości
                _ => throw new NotSupportedException($"Kraj '{countryCode}' nie jest obsługiwany.")
            };
        }
    }
}
