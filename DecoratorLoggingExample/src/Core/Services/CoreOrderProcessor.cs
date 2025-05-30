using DecoratorLoggingExample.src.Core.Interfaces;
using DecoratorLoggingExample.src.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorLoggingExample.src.Core.Services
{
    public class CoreOrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"[CORE] Przetwarzam zamówienie #{order.Id}: {order.Quantity}×{order.Product}");
        }
    }
}
