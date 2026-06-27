using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class ApiResponse<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? Error { get; init; }
    }
}
