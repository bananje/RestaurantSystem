using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Events;

public record OrderLinesNotConfirmed
{
    public IList<(OrderLine OrderLine, string Reason)> OrderLines { get; init; } = [];
}
