using LuckyFoodSystem.Orders.Bll.Services;
using LuckyFoodSystem.Orders.Infrastructure.Common;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using Marten;
using Newtonsoft.Json;
using System.Text;

namespace LuckyFoodSystem.Orders.Infrastructure.EventSourcing;

public class EventStoreDbClient(
    IDocumentSession session) : IEventSourcingClient
{
    public async Task AppendEventAsync(
        IDomainEvent @event
    )
    {
        session.Events.StartStream<IAggregateRoot>(
               @event.AggregateId,
               SerializeEvent(@event));

        await session.SaveChangesAsync();
    }

    public async Task<(long Version, IEnumerable<IDomainEvent> Events)> ReadEventsAsync(Guid aggregateId)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentNullException(nameof(aggregateId));

        var persistedEvents = new List<IDomainEvent>();
        long aggregateVersion = -1;

        var events = await session.Events.FetchStreamAsync(aggregateId);

        if (events == null || events.Count == 0)
            return (aggregateVersion, persistedEvents);

        foreach (var e in events)
        {
            var domainEvent = DeserializeEvent(e.EventType.Name, (ReadOnlyMemory<byte>) e.Data);
            persistedEvents.Add(domainEvent);
            aggregateVersion = e.Version;
        }

        return (aggregateVersion, persistedEvents);
    }

    private static byte[] SerializeEvent(IDomainEvent @event)
           => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

    private static IDomainEvent DeserializeEvent(string eventType, ReadOnlyMemory<byte> data)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return (IDomainEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data.ToArray()), Type.GetType(eventType), settings)!;
    }
}
