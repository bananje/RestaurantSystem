namespace LuckyFoodSystem.Shared.Domain.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {

        }
        protected AggregateRoot() { }

        readonly ICollection<DomainEvent> _uncommittedEvents = new List<DomainEvent>();

        public void MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        protected abstract void Apply(DomainEvent @event);

        public void RaiseEvent(DomainEvent @event)
        {
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges() => _uncommittedEvents;
    }
}
