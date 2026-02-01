using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Domain.Abstractions
{
    public class Result
    {
        public bool isSuccess { get; init; }
        public string? ErrorMessage { get; init; } = string.Empty;

        protected Result(bool isSuccess, string? errorMessage)
        {
            this.isSuccess = isSuccess;
            this.ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }
        public static Result Failure(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }
}
