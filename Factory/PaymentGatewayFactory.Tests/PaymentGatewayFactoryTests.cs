using Microsoft.Extensions.DependencyInjection;
using PaymentGatewayFactory.Application.Factories;
using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using PaymentGatewayFactory.Infrastructure.Gateways;
using System;
using Xunit;
using static PaymentGatewayFactory.Domain.Models.CountryCode;

namespace PaymentGatewayFactory.Tests
{
    public class PaymentGatewayFactoryTests
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentGatewayFactoryTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<PayUGateway>();
            services.AddTransient<PayPalGateway>();
            services.AddSingleton<IPaymentGatewayFactory, Application.Factories.PaymentGatewayFactory>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void GetGateway_ForPL_ReturnsPayUGateway()
        {
            // Arrange
            var factory = _serviceProvider.GetRequiredService<IPaymentGatewayFactory>();
            
            // Act
            var gateway = factory.GetGateway(PL);
            
            // Assert
            Assert.NotNull(gateway);
            Assert.IsType<PayUGateway>(gateway);
        }

        [Fact]
        public void GetGateway_ForUS_ReturnsPayPalGateway()
        {
            // Arrange
            var factory = _serviceProvider.GetRequiredService<IPaymentGatewayFactory>();
            
            // Act
            var gateway = factory.GetGateway(US);
            
            // Assert
            Assert.NotNull(gateway);
            Assert.IsType<PayPalGateway>(gateway);
        }

        [Fact]
        public void GetGateway_ForUnsupportedCountry_ThrowsNotSupportedException()
        {
            // Arrange
            var factory = _serviceProvider.GetRequiredService<IPaymentGatewayFactory>();
            
            // Act & Assert
            // Używamy wartości spoza zakresu enuma
            var exception = Assert.Throws<NotSupportedException>(() => factory.GetGateway((CountryCode)999));
            Assert.Contains("nie jest obsługiwany", exception.Message);
        }
    }
}
