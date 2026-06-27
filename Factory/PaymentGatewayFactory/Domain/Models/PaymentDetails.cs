using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Domain.Models
{
    /// <summary>
    /// Klasa przechowująca podstawowe informacje o płatności (np. dane karty).
    /// </summary>
    public class PaymentDetails
    {
        public string CardNumber { get; set; } = default!;
        public string CardHolderName { get; set; } = default!;
        public string ExpiryMonth { get; set; } = default!;  
        public string ExpiryYear { get; set; } = default!;  
        public string Cvv { get; set; } = default!;    
    }
}
