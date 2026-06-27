using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public sealed class WeatherReport
    {
        public int Temperature { get; init; }
        public bool IsRaining { get; init; }
        public int WindSpeed { get; init; }
    }
}
