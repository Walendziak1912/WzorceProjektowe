using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionPatternDatabaseExample.Application.Configuration;
using OptionPatternDatabaseExample.Application.Interfaces;
using OptionPatternDatabaseExample.Application.Services;
using OptionPatternDatabaseExample.Domain.Interfaces;
using OptionPatternDatabaseExample.Infrastructure.Configuration;
using OptionPatternDatabaseExample.Infrastructure.Repositories;
using OptionPatternDatabaseExample.Infrastructure.Services;

namespace OptionPatternDatabaseExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Konfiguracja z walidacją
                services.Configure<DatabaseOptions>(
                    context.Configuration.GetSection(DatabaseOptions.SectionName));

                services.Configure<EmailOptions>(
                    context.Configuration.GetSection(EmailOptions.SectionName));

                services.Configure<BusinessOptions>(
                    context.Configuration.GetSection(BusinessOptions.SectionName));

                // Dodanie walidacji
                services.AddOptions<DatabaseOptions>()
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

                services.AddOptions<EmailOptions>()
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

                services.AddOptions<BusinessOptions>()
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

                // Application Services
                services.AddScoped<IUserService, UserService>();

                // Infrastructure Services
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IEmailService, EmailService>();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            })
            .Build();

            try
            {
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Application starting...");

                var userService = host.Services.GetRequiredService<IUserService>();
                await userService.ProcessUsersAsync();

                logger.LogInformation("Application completed successfully");
            }
            catch (Exception ex)
            {
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogCritical(ex, "Application terminated unexpectedly");
                throw;
            }
            finally
            {
                await host.StopAsync();
            }
        }
    }
}
