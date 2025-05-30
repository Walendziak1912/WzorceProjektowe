using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorLoggingExample.src.Core.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
