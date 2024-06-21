using FluentValidation;
using LuckyFoodSystem.Orders.Bll.Extensions;
using LuckyFoodSystem.Orders.Bll.Features.Behaivors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LuckyFoodSystem.Orders.Bll;

public static class DependencyInjection 
{
    public static IServiceCollection AddBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMapsterConfiguration();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
