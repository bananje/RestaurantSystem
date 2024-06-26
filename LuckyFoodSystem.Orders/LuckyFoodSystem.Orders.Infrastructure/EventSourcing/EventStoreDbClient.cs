using LuckyFoodSystem.Orders.Bll.Services;
using LuckyFoodSystem.Orders.Infrastructure.Common;
using LuckyFoodSystem.Orders.Infrastructure.Extensions;
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
        SeedConfiguration.SeedDataAsync(session.DocumentStore);

        if (aggregateId == Guid.Empty)
            throw new ArgumentNullException(nameof(aggregateId));

        var persistedEvents = new List<IDomainEvent>();
        long aggregateVersion = -1;

        var events = await session.Events.FetchStreamAsync(aggregateId);

        if (events == null || events.Count == 0)
            return (aggregateVersion, persistedEvents);

        foreach (var e in events)
        {
            var domainEvent = DeserializeEvent((byte[]) e.Data);
            persistedEvents.Add(domainEvent);
            aggregateVersion = e.Version;
        }

        return (aggregateVersion, persistedEvents);
    }

    private static byte[] SerializeEvent(IDomainEvent @event)
    {
        var eventType = @event.GetType().AssemblyQualifiedName;
        var eventData = new
        {
            EventType = eventType,
            Data = @event
        };

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventData, settings));
    }

    private static IDomainEvent DeserializeEvent(ReadOnlyMemory<byte> data)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        var eventData = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(data.ToArray()), settings);
        string type = eventData.EventType;
        string eventJson = eventData.Data.ToString();
        return (IDomainEvent)JsonConvert.DeserializeObject(eventJson, Type.GetType(type)!, settings)!;
    }
}
