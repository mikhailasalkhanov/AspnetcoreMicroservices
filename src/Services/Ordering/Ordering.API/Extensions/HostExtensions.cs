using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host,
        Action<TContext?, IServiceProvider> seeder, int retry = 0) where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation
                ("Migrating database associated with context {DbContextName}", typeof(TContext));
            InvokeSeeder(seeder, context, services);
            logger.LogInformation
                ("Migrated database associated with context {DbContextName}", typeof(TContext));
        }
        catch (SqlException e)
        {
            logger.LogError
                ("An error occured while migrating database associated with context {DbContextName}",
                    typeof(TContext));
            if (retry < 50)
            {
                retry++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, seeder, retry);
            }
        }

        return host;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
        TContext context, IServiceProvider services) where TContext : DbContext?
    {
        context?.Database.Migrate();
        seeder(context, services);
    }
}