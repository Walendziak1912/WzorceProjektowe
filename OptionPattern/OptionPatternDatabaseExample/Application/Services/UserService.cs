using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionPatternDatabaseExample.Application.Configuration;
using OptionPatternDatabaseExample.Application.Interfaces;
using OptionPatternDatabaseExample.Domain.Entities;
using OptionPatternDatabaseExample.Domain.Interfaces;
using OptionPatternDatabaseExample.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPatternDatabaseExample.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;
        private readonly BusinessOptions _businessOptions;

        public UserService(
            IUserRepository userRepository,
            IEmailService emailService,
            ILogger<UserService> logger,
            IOptions<BusinessOptions> businessOptions)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _logger = logger;
            _businessOptions = businessOptions.Value;
        }

        public async Task ProcessUsersAsync()
        {
            _logger.LogInformation("Starting user processing...");

            var users = await _userRepository.GetActiveUsersAsync();

            foreach (var user in users)
            {
                if (ShouldProcessUser(user))
                {
                    await ProcessSingleUserAsync(user);
                    await _emailService.SendNotificationAsync(user.Email, "Processing completed");
                }
            }

            _logger.LogInformation($"Processed {users.Count} users");
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        private bool ShouldProcessUser(User user)
        {
            return user.IsActive && user.CreatedAt > DateTime.Now.AddDays(-_businessOptions.MaxDaysOld);
        }

        private async Task ProcessSingleUserAsync(User user)
        {
            // Business logic here
            _logger.LogDebug($"Processing user {user.Id}");
            await Task.Delay(100); // Simulate work
        }
    }
}
