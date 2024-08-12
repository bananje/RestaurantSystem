using LuckyFoodRestaurantSystem.Contracts.Shared;

namespace LuckyFoodRestaurantSystem.ProductStock.Contracts;

public record ProcessOrderArgument
{
    public IReadOnlyCollection<OrderLineRequestStruct> OrderLines { get; init; } = [];
}
