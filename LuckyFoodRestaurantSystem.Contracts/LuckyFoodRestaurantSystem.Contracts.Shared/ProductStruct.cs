namespace LuckyFoodRestaurantSystem.Contracts.Shared;
public record ProductStruct
{
    public Guid ProductId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string ProductImageUrl { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string ShortDescription { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public decimal Discount { get; init; }

    public decimal? BaseDiscount { get; init; }

    public decimal? MaxDiscount { get; init; }

    public decimal PriceWithDiscount { get; init; }

    public double Weight { get; init; }

    public string WeightUnit { get; init; } = string.Empty;
}
