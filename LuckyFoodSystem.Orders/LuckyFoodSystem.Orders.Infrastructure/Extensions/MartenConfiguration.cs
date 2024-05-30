using LuckyFoodSystem.Orders.Infrastructure.Options;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LuckyFoodSystem.Orders.Infrastructure.Extensions;

public static class MartenConfiguration
{
    public static IServiceCollection AddMartenConfiguration(
        this IServiceCollection services, 
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var martenConfig = configuration.GetSection(MartenConfig.SectionName);

        services.Configure<MartenConfig>(martenConfig);

        var config = serviceProvider.GetRequiredService<IOptions<MartenConfig>>().Value;

        services.AddMarten(opt =>
        {
            opt.Connection(config.ConnectionString);

            opt.Events.DatabaseSchemaName = config.DatabaseSchemaName;

            opt.Events.StreamIdentity = Marten.Events.StreamIdentity.AsString;
        });

        return services;
    }
}
