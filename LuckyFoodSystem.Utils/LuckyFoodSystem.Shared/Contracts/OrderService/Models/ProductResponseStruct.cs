namespace LuckyFoodSystem.Shared.Contracts.OrderService.Models;

public record ProductResponseStruct
{
    public Guid ProductId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string ProductImageUrl { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public decimal Discount { get; init; }

    public decimal PriceWithDiscount { get; init; }

    public double Weight { get; init; }

    public string WeightUnit { get; init; } = string.Empty;
}
