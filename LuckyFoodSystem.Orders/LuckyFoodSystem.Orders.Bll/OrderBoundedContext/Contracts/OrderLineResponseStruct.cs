namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;

public record OrderLineResponseStruct
{
    public Guid ProductId { get; init; }

    public string ProductTitle { get; init; } = string.Empty;

    public string ProductImageUrl { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Weight { get; init; } = string.Empty;

    public int Quantity { get; init; }
}
