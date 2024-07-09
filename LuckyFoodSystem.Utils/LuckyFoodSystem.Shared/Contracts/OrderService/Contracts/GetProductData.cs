using LuckyFoodSystem.Shared.Contracts.OrderService.Models;

namespace LuckyFoodSystem.Shared.Contracts.OrderService.Contracts;

public record GetProductDataRequest
{
    public Guid OrderLineId = Guid.NewGuid();

    public Guid OrderId { get; init; }

    public Guid ProductId { get; init; }

    public int Quantity { get; init; }
}

public record GetProductDataResponse
{
    public Guid OrderId { get; init; }

    public Guid OrderLineId { get; init; }

    public ProductResponseStruct Product { get; init; } = null!;

    public int Quantity { get; init; }
}