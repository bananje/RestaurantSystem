using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Services;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderDbContext;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Repositories;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Services;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.EventSourcing;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.EventSourcing.Repositories;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure;

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

        services.AddTransient<IProductService, ProductService>();

        services.AddTransient(typeof(IQueryRepository<>), typeof(ProjectionRepository<>));

        return services;
    }
}
