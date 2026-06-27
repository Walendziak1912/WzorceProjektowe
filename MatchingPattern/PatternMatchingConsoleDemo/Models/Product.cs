using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class Product
    {
        public required string Name { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
