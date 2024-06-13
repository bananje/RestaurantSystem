using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Context;
using LuckyFoodSystem.Orders.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
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

        services.AddMassTransitConfiguration(serviceProvider, configuration);

        services.AddDbContext<OrdersDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("QueryDbConnection"));
        });

        return services;
    }
}
