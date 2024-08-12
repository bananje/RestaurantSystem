namespace LuckyFoodRestaurantSystem.Contracts.Shared;

public record OrderLineResponseStruct
{
    public ProductStruct Product { get; init; } = null!;

    public int Quantity { get; init; }
}
