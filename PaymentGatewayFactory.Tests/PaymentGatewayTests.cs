using PaymentGatewayFactory.Domain.Models;
using PaymentGatewayFactory.Infrastructure.Gateways;
using System;
using System.IO;
using Xunit;

namespace PaymentGatewayFactory.Tests
{
    public class PaymentGatewayTests
    {
        [Fact]
        public void PayUGateway_Charge_ExecutesWithoutException()
        {
            // Arrange
            var gateway = new PayUGateway();
            var details = new PaymentDetails
            {
                CardNumber = "4111111111111111",
                CardHolderName = "Jan Kowalski",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            decimal amount = 100.00m;

            // Act & Assert - sprawdzamy, czy metoda nie rzuca wyjątku
            var exception = Record.Exception(() => gateway.Charge(amount, details));
            Assert.Null(exception);
        }

        [Fact]
        public void PayUGateway_Refund_ExecutesWithoutException()
        {
            // Arrange
            var gateway = new PayUGateway();
            string transactionId = "tx123_pl";

            // Act & Assert - sprawdzamy, czy metoda nie rzuca wyjątku
            var exception = Record.Exception(() => gateway.Refund(transactionId));
            Assert.Null(exception);
        }

        [Fact]
        public void PayUGateway_GetTransactionStatus_ReturnsExpectedStatus()
        {
            // Arrange
            var gateway = new PayUGateway();
            string transactionId = "tx123_pl";

            // Act
            var status = gateway.GetTransactionStatus(transactionId);

            // Assert
            Assert.Equal("COMPLETED", status);
        }

        [Fact]
        public void PayPalGateway_Charge_ExecutesWithoutException()
        {
            // Arrange
            var gateway = new PayPalGateway();
            var details = new PaymentDetails
            {
                CardNumber = "5500000000000004",
                CardHolderName = "Anna Nowak",
                ExpiryMonth = "06",
                ExpiryYear = "2026",
                Cvv = "456"
            };
            decimal amount = 200.00m;

            // Act & Assert - sprawdzamy, czy metoda nie rzuca wyjątku
            var exception = Record.Exception(() => gateway.Charge(amount, details));
            Assert.Null(exception);
        }

        [Fact]
        public void PayPalGateway_Refund_ExecutesWithoutException()
        {
            // Arrange
            var gateway = new PayPalGateway();
            string transactionId = "tx456_us";

            // Act & Assert - sprawdzamy, czy metoda nie rzuca wyjątku
            var exception = Record.Exception(() => gateway.Refund(transactionId));
            Assert.Null(exception);
        }

        [Fact]
        public void PayPalGateway_GetTransactionStatus_ReturnsExpectedStatus()
        {
            // Arrange
            var gateway = new PayPalGateway();
            string transactionId = "tx456_us";

            // Act
            var status = gateway.GetTransactionStatus(transactionId);

            // Assert
            Assert.Equal("SUCCESS", status);
        }

        [Fact]
        public void PayUGateway_Charge_WritesToConsole()
        {
            // Arrange
            var gateway = new PayUGateway();
            var details = new PaymentDetails
            {
                CardNumber = "4111111111111111",
                CardHolderName = "Jan Kowalski",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            decimal amount = 100.00m;

            // Przekierowanie Console.Out do StringWriter, aby przechwycić output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            gateway.Charge(amount, details);
            var output = stringWriter.ToString();

            // Assert
            Assert.Contains("[PayU]", output);
            Assert.Contains("Charge", output);
            Assert.Contains("4111111111111111", output);
        }

        [Fact]
        public void PayPalGateway_Charge_WritesToConsole()
        {
            // Arrange
            var gateway = new PayPalGateway();
            var details = new PaymentDetails
            {
                CardNumber = "5500000000000004",
                CardHolderName = "Anna Nowak",
                ExpiryMonth = "06",
                ExpiryYear = "2026",
                Cvv = "456"
            };
            decimal amount = 200.00m;

            // Przekierowanie Console.Out do StringWriter, aby przechwycić output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            gateway.Charge(amount, details);
            var output = stringWriter.ToString();

            // Assert
            Assert.Contains("[PayPal]", output);
            Assert.Contains("Charge", output);
            Assert.Contains("5500000000000004", output);
        }
    }
}
