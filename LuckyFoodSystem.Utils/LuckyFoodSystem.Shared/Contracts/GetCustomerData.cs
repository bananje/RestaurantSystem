namespace LuckyFoodSystem.Shared.Contracts;

public record GetCustomerData
{
    public Guid CustomerId { get; init; }
}
