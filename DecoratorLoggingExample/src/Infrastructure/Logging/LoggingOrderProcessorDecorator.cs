using DecoratorLoggingExample.src.Core.Interfaces;
using DecoratorLoggingExample.src.Core.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorLoggingExample.src.Infrastructure.Logging
{
    public class LoggingOrderProcessorDecorator : IOrderProcessor
    {
        private readonly IOrderProcessor _inner;
        private readonly ILogger<LoggingOrderProcessorDecorator> _logger;

        public LoggingOrderProcessorDecorator(
            IOrderProcessor inner,
            ILogger<LoggingOrderProcessorDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public void ProcessOrder(Order order)
        {
            _logger.LogInformation("► Rozpoczynam przetwarzanie zamówienia #{OrderId}", order.Id);
            try
            {
                _inner.ProcessOrder(order);
                _logger.LogInformation("✔ Zakończono przetwarzanie zamówienia #{OrderId}", order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "✖ Błąd podczas przetwarzania zamówienia #{OrderId}: {Message}",
                    order.Id, ex.Message);

                // Opcjonalnie: można tu wykonać dodatkowe kroki (czyszczenie zasobów, powiadomienia...)
                throw;
            }
        }
    }

}
