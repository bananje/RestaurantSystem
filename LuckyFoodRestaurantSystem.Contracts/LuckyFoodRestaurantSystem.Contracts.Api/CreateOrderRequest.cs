using LuckyFoodRestaurantSystem.Contracts.Shared;

namespace LuckyFoodRestaurantSystem.Contracts.Api;

public record CreateOrderRequest
{
    public Guid CustomerId { get; init; }

    public IReadOnlyCollection<OrderLineRequestStruct> OrderLines { get; init; } = [];
}
