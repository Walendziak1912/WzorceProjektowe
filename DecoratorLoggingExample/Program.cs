using DecoratorLoggingExample.src.Core.Interfaces;
using DecoratorLoggingExample.src.Core.Model;
using DecoratorLoggingExample.src.Core.Services;
using DecoratorLoggingExample.src.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DecoratorLoggingExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                services.AddTransient<IOrderProcessor, CoreOrderProcessor>();

                //logowanie
                services.AddLogging(cfg =>
                {
                    cfg.AddConsole();        
                    cfg.SetMinimumLevel(LogLevel.Information);
                });

                //Dekorujemy CoreOrderProcessor
                services.Decorate<IOrderProcessor, LoggingOrderProcessorDecorator>();
            })
            .Build();

            using var scope = host.Services.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();

            var order = new Order { Id = 42, Product = "Książka", Quantity = 3 };
            processor.ProcessOrder(order);
        }
    }
}
