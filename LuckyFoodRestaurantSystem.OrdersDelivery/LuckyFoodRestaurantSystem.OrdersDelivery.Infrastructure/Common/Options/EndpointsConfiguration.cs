namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Common.Options;

public class EndpointsConfiguration
{
    public static string SectionName = nameof(EndpointsConfiguration);

    public string OrderStateMachineAddress { get; set; } = string.Empty;

    public string UserServiceAddress { get; set; } = string.Empty;

    public string ProductServiceAddress { get; set; } = string.Empty;
}
