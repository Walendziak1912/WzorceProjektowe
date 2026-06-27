using System;
using System.Collections.Generic;
using System.Text;

namespace PatternMatchingConsoleDemo.Models
{
    public abstract record Result;
    public sealed record Success(string Message) : Result;
    public sealed record ValidationError(string Error) : Result;
    public sealed record Unauthorized() : Result;
    public sealed record NotFound() : Result;
}
