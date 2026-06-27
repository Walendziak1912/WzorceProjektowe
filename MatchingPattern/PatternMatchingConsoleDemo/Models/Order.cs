using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class Order
    {
        public required Customer Customer { get; init; }
        public required List<Product> Items { get; init; }
        public required Payment Payment { get; init; }
        public required Shipment Shipment { get; init; }
        public OrderStatus Status { get; init; }
        public decimal Total { get; init; }
        public bool IsOverdue { get; init; }
    }
}
