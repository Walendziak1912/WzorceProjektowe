using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class Shipment
    {
        public required string Country { get; init; }
        public required string City { get; init; }
        public bool Express { get; init; }
    }
}
