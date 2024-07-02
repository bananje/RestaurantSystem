namespace LuckyFoodSystem.Shared.Contracts;

public record GetProductData
{
    public ICollection<Guid> ProductIds { get; init; } = [];
}
