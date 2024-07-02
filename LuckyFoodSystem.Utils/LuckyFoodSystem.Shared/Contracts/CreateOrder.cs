using LuckyFoodSystem.Shared.Contracts.Models;

namespace LuckyFoodSystem.Shared.Contracts;

public record CreateOrder
{
    public Guid OrderId { get; private set; }

    public Guid CustomerId { get; init; }

    public ICollection<OrderLineRequestStruct> OrderLines { get; init; } = [];
}
