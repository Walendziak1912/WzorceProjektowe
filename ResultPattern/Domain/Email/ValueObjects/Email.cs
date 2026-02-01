using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultPattern.Domain.Email.ValueObjects
{
    public class Email
    {
        public string Address { get; init; } = string.Empty;
        public Email(string address) 
        {
            Address = address;
        }
    }
}
