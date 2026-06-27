using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Infrastructure.Configuration
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";

        [Required]
        [MinLength(10)]
        public string ConnectionString { get; set; } = string.Empty;

        [Range(1, 3600)]
        public int CommandTimeout { get; set; } = 30;

        public bool EnableRetryOnFailure { get; set; } = true;
    }
}
