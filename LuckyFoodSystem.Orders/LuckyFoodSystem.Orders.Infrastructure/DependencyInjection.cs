using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Services;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Context;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Repositories;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Services;
using LuckyFoodSystem.Orders.Infrastructure.EventSourcing;
using LuckyFoodSystem.Orders.Infrastructure.EventSourcing.Repositories;
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
