using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Events;

public record OrderCreated
{
    public Guid OrderId = Guid.NewGuid();

    public Guid CustomerId { get; init; }

    public IList<OrderLine> OrderLines { get; init; } = [];
}