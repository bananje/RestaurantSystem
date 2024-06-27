using LuckyFoodSystem.OrdersDelivery.Infrastructure.Options;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;

public static class MartenConfiguration
{
    public static IServiceCollection AddMartenConfiguration(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.Configure<MartenConfig>(configuration.GetSection(MartenConfig.SectionName));

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var config = serviceProvider.GetRequiredService<IOptions<MartenConfig>>().Value;

            string connectionString = config.ConnectionString;

            SeedConfiguration.EnsureDatabase(connectionString, config.DatabaseSchemaName);

            services.AddMarten(opt =>
            {
                opt.Connection(connectionString);
                opt.Events.StreamIdentity = StreamIdentity.AsGuid;
                opt.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
            });
        }

        return services;
    }
}
