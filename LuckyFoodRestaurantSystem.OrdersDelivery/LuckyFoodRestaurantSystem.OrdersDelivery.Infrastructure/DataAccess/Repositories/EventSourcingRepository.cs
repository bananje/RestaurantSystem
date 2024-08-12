using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using MediatR;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.DataAccess.Repositories;

public class EventSourcingRepository<TAggregate>(
    IEventSourcingClient client,
    IPublisher publisher) : IEventSourcingRepository<TAggregate>
        where TAggregate : class, IAggregateRoot, new()
{
    public async Task<TAggregate> FindByIdAsync(
        Guid aggregateId,
        CancellationToken cancellationToken = default)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentNullException(nameof(aggregateId));

        var result = await client.ReadEventsAsync(aggregateId);

        if (result.Version == -1)
        {
            return default!;
        }

        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate))!;

        aggregate.LoadFromHistory(result.Version, result.Events);

        return aggregate;
    }

    public async Task SaveAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken = default)
    {
        var aggregateVersion = aggregate.Version;

        foreach (var @event in aggregate.GetUncommittedChanges())
        {
            @event.AggregateVersion = aggregateVersion++;
            await client.AppendEventAsync<TAggregate>(@event);

            await publisher.Publish((dynamic)@event);
        }

        aggregate.MarkChangesAsCommitted();
    }
}
