using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Services;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.EventSourcing.Repositories;

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

        if (result == default)
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
            await client.AppendEventAsync(@event);

            await publisher.Publish((dynamic)@event);
        }
        aggregate.MarkChangesAsCommitted();
    }
}
