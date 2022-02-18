using DataAccess.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Wep.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = default;
            ILogger<Program> logger = default;

            try
            {
                host = CreateHostBuilder(args).Build();
                logger = CreateLogger(host.Services);
                await ApplyMigrations(host.Services);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            host.Run();
        }

        private static ILogger<Program> CreateLogger(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            return scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        }

        private static async Task ApplyMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

            await dbContext.Database.MigrateAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
