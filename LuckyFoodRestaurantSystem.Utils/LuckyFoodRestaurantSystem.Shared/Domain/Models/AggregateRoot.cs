using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using System.Text.Json.Serialization;

namespace LuckyFoodSystem.Shared.Domain.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {

        }
        protected AggregateRoot() { }

        [JsonIgnore] readonly ICollection<IDomainEvent> _uncommittedEvents = new List<IDomainEvent>();

        public void MarkChangesAsCommitted()
        {
            _uncommittedEvents.Clear();
        }

        protected abstract void Apply(IDomainEvent @event);

        public void RaiseEvent(IDomainEvent @event)
        {
            _uncommittedEvents.Add(@event);
        }

        public IEnumerable<IDomainEvent> GetUncommittedChanges() => _uncommittedEvents;

        public void LoadFromHistory(long version, IEnumerable<IDomainEvent> events)
        {
            Version = version;

            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public long Version { get; set; }
    }
}
