using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayFactory.Infrastructure.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Metoda rozszerzająca, która rejestruje w kontenerze DI wszystkie implementacje bramek płatności.
        /// </summary>
        public static IServiceCollection AddPaymentGateways(this IServiceCollection services)
        {
            // Rejestrujemy poszczególne bramki jako transient (za każdym razem nowa instancja)
            services.AddTransient<PayUGateway>();
            services.AddTransient<PayPalGateway>();
            // Jeśli będą kolejne bramki, rejestrujesz je tutaj.

            return services;
        }
    }
}
