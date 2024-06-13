using LuckyFoodSystem.Orders.Bll.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyFoodSystem.Orders.Bll;

public static class DependencyInjection 
{
    public static IServiceCollection AddBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMapsterConfiguration();

        return services;
    }
}
