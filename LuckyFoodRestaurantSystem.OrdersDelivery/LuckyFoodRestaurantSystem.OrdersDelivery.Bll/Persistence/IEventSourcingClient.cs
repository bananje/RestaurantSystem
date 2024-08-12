using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;

public interface IEventSourcingClient
{
    Task<(long Version, IEnumerable<IDomainEvent> Events)> ReadEventsAsync(Guid aggregateId);

    Task AppendEventAsync<T>(IDomainEvent @event);
}
