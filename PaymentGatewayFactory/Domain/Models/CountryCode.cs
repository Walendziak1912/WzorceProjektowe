using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayFactory.Domain.Models
{
    /// <summary>
    /// Enum reprezentujący kody krajów używane do wyboru odpowiedniej bramki płatności.
    /// </summary>
    public enum CountryCode
    {
        /// <summary>
        /// Polska - używa bramki PayU
        /// </summary>
        PL,
        
        /// <summary>
        /// Stany Zjednoczone - używa bramki PayPal
        /// </summary>
        US
    }
}
