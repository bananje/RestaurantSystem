namespace LuckyFoodSystem.Shared.Domain.Models.Contracts;

public interface IAggregateRoot
{
    public long Version { get; }

    /// <summary>
    /// Apply given events in order to rebuild aggregate
    /// </summary>
    public void LoadFromHistory(long version, IEnumerable<IDomainEvent> history);

    /// <summary>
    /// List all new events to be persisted in the event store
    /// </summary>
    public IEnumerable<IDomainEvent> GetUncommittedChanges();

    /// <summary>
    /// Clear all new events to be persisted in the event store
    /// </summary>
    public void MarkChangesAsCommitted();
}
