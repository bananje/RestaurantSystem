using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.Shared.Domain.Models;

public class DomainEvent : IDomainEvent
{
    /// <summary>
    /// The event identifier
    /// </summary>
    public Guid EventId { get; private set; }

    /// <summary>
    /// The identifier of the aggregate which has generated the event
    /// </summary>
    public Guid AggregateId { get; protected set; }

    /// <summary>
    /// The version of the aggregate when the event has been generated
    /// </summary>
    public long AggregateVersion { get; set; }

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
    }

    protected DomainEvent(Guid aggregateId) : this()
    {
        AggregateId = aggregateId;
    }

    protected DomainEvent(Guid aggregateId, long aggregateVersion) : this(aggregateId)
    {
        AggregateVersion = aggregateVersion;
    }
}
