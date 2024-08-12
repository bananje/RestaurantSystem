namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Common.Options;

public class RabbitMqConfig
{
    public static string SectionName = nameof(RabbitMqConfig);

    public string Host { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
