using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Shared.Domain.Models
{
    public abstract class AggregateRoot<TId, TEvent> : Entity<TId>
        where TId : notnull
        where TEvent : IDomainEvent
    {
        protected AggregateRoot(TId id) : base(id)
        {

        }
        protected AggregateRoot() { }

        readonly ICollection<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

        public void MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        protected abstract void Apply(TEvent @event);

        public void RaiseEvent(TEvent @event)
        {
            Apply(@event);
            _uncommittedEvents.Add(@event);
        }

        public IEnumerable<IDomainEvent> GetUncommittedChanges() => _uncommittedEvents;
    }
}
