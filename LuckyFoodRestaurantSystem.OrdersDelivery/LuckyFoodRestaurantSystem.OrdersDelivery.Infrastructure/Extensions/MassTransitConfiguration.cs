using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Consumers;
using LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Common.Options;
using LuckyFoodRestaurantSystem.ProductStock.Contracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMassTransitConfiguration(
        this IServiceCollection services,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var brokerConfig = configuration.GetSection(RabbitMqConfig.SectionName);

        services.Configure<RabbitMqConfig>(brokerConfig);

        var config = serviceProvider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

        var endpointsSection = configuration.GetSection("EndpointsConfiguration");
        var endpointsConfig = endpointsSection.Get<EndpointsConfiguration>();

        services.Configure<EndpointsConfiguration>(endpointsSection);

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseJsonSerializer();

                cfg.Host(config.Host, "/", h => {
                    h.Username(config.UserName);
                    h.Password(config.Password);
                });

                cfg.ConfigureEndpoints(context);
            });

            x.AddConsumer<GetOrderLinesConsumer>()
                .Endpoint(e => e.Name = "product-service");

            x.AddRequestClient<GetOrderLinesRequest>(new Uri(endpointsConfig!.ProductServiceAddress));

            x.AddConsumer<CustomerActualizingDataConsumer>();
        });

        return services;
    }
}
