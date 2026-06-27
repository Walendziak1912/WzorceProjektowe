using OptionPatternDatabaseExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Domain.Services
{
    // Tylko czysta logika biznesowa bez zależności zewnętrznych
    public class UserDomainService
    {
        public bool ShouldProcessUser(User user)
        {
            return user.IsActive && user.CreatedAt > DateTime.Now.AddDays(-30);
        }
    }
}
