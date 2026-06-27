using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class Customer
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public bool IsVip { get; init; }
        public string Country { get; init; } = "PL";
    }
}
