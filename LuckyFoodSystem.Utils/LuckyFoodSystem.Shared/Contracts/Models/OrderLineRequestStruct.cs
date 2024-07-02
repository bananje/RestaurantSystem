namespace LuckyFoodSystem.Shared.Contracts.Models;

public record OrderLineRequestStruct
{
    public Guid ProductId { get; init; }

    public int Quantity { get; init; }
}