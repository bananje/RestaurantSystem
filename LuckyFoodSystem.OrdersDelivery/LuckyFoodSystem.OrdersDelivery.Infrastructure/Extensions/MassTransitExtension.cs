using LuckyFoodSystem.OrdersDelivery.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitConfiguration(
        this IServiceCollection services,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var brokerConfig = configuration.GetSection(RabbitMqConfig.SectionName);

        services.Configure<RabbitMqConfig>(brokerConfig);

        var config = serviceProvider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config.Host, "/", h => {
                    h.Username(config.UserName);
                    h.Password(config.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
