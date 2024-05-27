using LuckyFoodSystem.Orders.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyFoodSystem.Orders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        services.AddMartenConfiguration(serviceProvider, configuration);

        return services;
    }
}
