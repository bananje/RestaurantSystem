namespace LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;

public record RejectOrderArgument
{
    public Guid OrderId { get; init; }

    public string Reason { get; init; } = string.Empty;
}
