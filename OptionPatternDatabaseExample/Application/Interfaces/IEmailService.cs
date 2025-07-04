﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendNotificationAsync(string email, string message);
    }
}
