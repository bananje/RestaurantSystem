using MediatR;

namespace LuckyFoodSystem.Shared.Domain.Models.Contracts;

public interface IDomainEvent : INotification
{

    /// <summary>
    /// The event identifier
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// The identifier of the aggregate which has generated the event
    /// </summary>
    public Guid AggregateId { get; }

    /// <summary>
    /// The version of the aggregate when the event has been generated
    /// </summary>
    public long AggregateVersion { get; set; }
}
