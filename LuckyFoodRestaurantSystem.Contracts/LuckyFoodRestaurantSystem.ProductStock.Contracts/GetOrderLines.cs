using LuckyFoodRestaurantSystem.Contracts.Shared;

namespace LuckyFoodRestaurantSystem.ProductStock.Contracts;

public record GetOrderLinesRequest
{
    public IReadOnlyCollection<OrderLineRequestStruct> OrderLines { get; init; } = [];
}

public record GetOrderLinesResponse
{
    public IList<OrderLineResponseStruct> OrderLines { get; init; } = [];

    public string? Reason { get; init; }

    public bool IsConfirmed { get; init; } = false;
}