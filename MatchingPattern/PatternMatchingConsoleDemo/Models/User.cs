using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class User
    {
        public required string Name { get; init; }
        public UserRole Role { get; init; }
        public bool IsActive { get; init; }
        public bool IsAdmin { get; init; }
        public string Department { get; init; } = "";
    }
}
