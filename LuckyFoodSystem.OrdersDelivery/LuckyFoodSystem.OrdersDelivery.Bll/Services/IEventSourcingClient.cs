using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.OrdersDelivery.Bll.Services;

public interface IEventSourcingClient
{
    Task<(long Version, IEnumerable<IDomainEvent> Events)> ReadEventsAsync(Guid aggregateId);

    Task AppendEventAsync(IDomainEvent @event);
}
