using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.DataAccess.Repositories;
using LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Services;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Repositories;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        services.AddMartenConfiguration(configuration);

        services.AddMassTransitConfiguration(serviceProvider, configuration);

        services.AddDbContext<OrdersDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("QueryDbConnection"));
        });

        services.AddTransient(typeof(IEventSourcingRepository<>), typeof(EventSourcingRepository<>));

        services.AddTransient(typeof(IProjectionRepository<>), typeof(ProjectionRepository<>));

        services.AddTransient<IEventSourcingClient, EventStoreDbClient>();

        services.AddTransient(typeof(IQueryRepository<>), typeof(ProjectionRepository<>));

        return services;
    }
}
