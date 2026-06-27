using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class Payment
    {
        public PaymentStatus Status { get; init; }
        public decimal Amount { get; init; }
        public int RetryCount { get; init; }
    }
}
