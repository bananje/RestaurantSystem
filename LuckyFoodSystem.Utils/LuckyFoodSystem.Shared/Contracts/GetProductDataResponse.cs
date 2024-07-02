namespace LuckyFoodSystem.Shared.Contracts;

public record GetProductDataResponse
{
    public string Title { get; init; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public string ProductImageUrl { get; private set; } = string.Empty;

    public string Status { get; private set; } = string.Empty;

    public string ShortDescription { get; private set; } = string.Empty;

    public double Price { get; private set; }

    public double Discount { get; private set; }

    public decimal? BaseDiscount { get; private set; } 

    public decimal? MaxDiscount { get; private set; }

    public string Weight { get; private set; } = string.Empty;

    public string WeightUnit { get; private set; } = string.Empty;
}
