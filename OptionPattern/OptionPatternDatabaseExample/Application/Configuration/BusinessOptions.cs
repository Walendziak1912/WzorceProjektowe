using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Application.Configuration
{
    public class BusinessOptions
    {
        public const string SectionName = "Business";

        public int MaxDaysOld { get; set; } = 30;
        public int BatchSize { get; set; } = 100;
        public bool SendEmailNotifications { get; set; } = true;
    }
}
