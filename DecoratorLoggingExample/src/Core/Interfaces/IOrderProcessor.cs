﻿using DecoratorLoggingExample.src.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorLoggingExample.src.Core.Interfaces
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Order order);
    }
}
