using Microsoft.Extensions.DependencyInjection;
using Moq;
using PaymentGatewayFactory.Application.Services;
using PaymentGatewayFactory.Domain.Interfaces;
using PaymentGatewayFactory.Domain.Models;
using System;
using Xunit;
using static PaymentGatewayFactory.Domain.Models.CountryCode;

namespace PaymentGatewayFactory.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void ProcessPayment_ForPL_UsesPayUGateway()
        {
            // Arrange
            var mockGateway = new Mock<IPaymentGateway>();
            var mockFactory = new Mock<IPaymentGatewayFactory>();
            mockFactory.Setup(f => f.GetGateway(PL)).Returns(mockGateway.Object);
            
            var service = new PaymentService(mockFactory.Object);
            var details = new PaymentDetails
            {
                CardNumber = "1234567890123456",
                CardHolderName = "Test User",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            
            // Act
            service.ProcessPayment(100.00m, PL, details);
            
            // Assert
            mockFactory.Verify(f => f.GetGateway(PL), Times.Once);
            mockGateway.Verify(g => g.Charge(100.00m, details), Times.Once);
        }

        [Fact]
        public void ProcessPayment_ForUS_UsesPayPalGateway()
        {
            // Arrange
            var mockGateway = new Mock<IPaymentGateway>();
            var mockFactory = new Mock<IPaymentGatewayFactory>();
            mockFactory.Setup(f => f.GetGateway(US)).Returns(mockGateway.Object);
            
            var service = new PaymentService(mockFactory.Object);
            var details = new PaymentDetails
            {
                CardNumber = "1234567890123456",
                CardHolderName = "Test User",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            
            // Act
            service.ProcessPayment(200.00m, US, details);
            
            // Assert
            mockFactory.Verify(f => f.GetGateway(US), Times.Once);
            mockGateway.Verify(g => g.Charge(200.00m, details), Times.Once);
        }

        [Fact]
        public void RefundPayment_CallsRefundOnGateway()
        {
            // Arrange
            var mockGateway = new Mock<IPaymentGateway>();
            var mockFactory = new Mock<IPaymentGatewayFactory>();
            mockFactory.Setup(f => f.GetGateway(PL)).Returns(mockGateway.Object);
            
            var service = new PaymentService(mockFactory.Object);
            string transactionId = "tx123";
            
            // Act
            service.RefundPayment(PL, transactionId);
            
            // Assert
            mockFactory.Verify(f => f.GetGateway(PL), Times.Once);
            mockGateway.Verify(g => g.Refund(transactionId), Times.Once);
        }

        [Fact]
        public void GetStatus_ReturnsStatusFromGateway()
        {
            // Arrange
            var mockGateway = new Mock<IPaymentGateway>();
            mockGateway.Setup(g => g.GetTransactionStatus("tx123")).Returns("COMPLETED");
            
            var mockFactory = new Mock<IPaymentGatewayFactory>();
            mockFactory.Setup(f => f.GetGateway(PL)).Returns(mockGateway.Object);
            
            var service = new PaymentService(mockFactory.Object);
            
            // Act
            var result = service.GetStatus(PL, "tx123");
            
            // Assert
            Assert.Equal("COMPLETED", result);
            mockFactory.Verify(f => f.GetGateway(PL), Times.Once);
            mockGateway.Verify(g => g.GetTransactionStatus("tx123"), Times.Once);
        }

        [Fact]
        public void ProcessPayment_WithInvalidCountry_ThrowsException()
        {
            // Arrange
            var mockFactory = new Mock<IPaymentGatewayFactory>();
            mockFactory.Setup(f => f.GetGateway((CountryCode)999)).Throws(new NotSupportedException("Kraj '999' nie jest obsługiwany."));
            
            var service = new PaymentService(mockFactory.Object);
            var details = new PaymentDetails
            {
                CardNumber = "1234567890123456",
                CardHolderName = "Test User",
                ExpiryMonth = "12",
                ExpiryYear = "2025",
                Cvv = "123"
            };
            
            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => service.ProcessPayment(100.00m, (CountryCode)999, details));
            Assert.Contains("999", exception.Message);
        }
    }
}
