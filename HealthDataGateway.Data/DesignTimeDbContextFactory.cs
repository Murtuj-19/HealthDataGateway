using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HealthDataGateway.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            // Determine base path for configuration. Try current directory, then known web project location, then walk up.
            var current = Directory.GetCurrentDirectory();
            string? basePath = null;

            // 1) current directory
            if (File.Exists(Path.Combine(current, "appsettings.json")))
            {
                basePath = current;
            }

            // 2) ../HealthDataGateway.Web
            if (basePath == null)
            {
                var candidate = Path.GetFullPath(Path.Combine(current, "..", "HealthDataGateway.Web"));
                if (File.Exists(Path.Combine(candidate, "appsettings.json")))
                    basePath = candidate;
            }

            // 3) walk up to 5 levels to find appsettings.json
            if (basePath == null)
            {
                var dir = current;
                for (int i = 0; i < 6; i++)
                {
                    var candidate = Path.Combine(dir, "appsettings.json");
                    if (File.Exists(candidate))
                    {
                        basePath = dir;
                        break;
                    }
                    dir = Path.GetFullPath(Path.Combine(dir, ".."));
                }
            }

            // 4) fallback to current (will cause later error if no connection string)
            basePath ??= current;

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
