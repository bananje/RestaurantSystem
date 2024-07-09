using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Events;

public record OrderConfirmed
{
    public Guid OrderId { get; init; }

    public Guid CustomerId { get; init; }

    public string City { get; init; } = string.Empty;

    public string Street { get; init; } = string.Empty;

    public string House { get; init; } = string.Empty;

    public string ApartmentNum { get; init; } = string.Empty;

    public ICollection<OrderLine> OrderLines { get; init; } = [];
}
