using OptionPatternDatabaseExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Application.Interfaces
{
    public interface IUserService
    {
        Task ProcessUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
    }
}
