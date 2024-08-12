using FluentValidation;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Behaivors;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMapperConfiguration();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
