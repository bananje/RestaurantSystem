using LuckyFoodRestaurantSystem.Contracts.Shared;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;

public record ConfirmOrderArgument
{
    public Guid OrderId { get; init; }

    public IReadOnlyCollection<OrderLineRequestStruct> OrderLines { get; init; } = [];
}
