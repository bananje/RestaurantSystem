using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IEventSourcingRepository<TAggregate> 
    where TAggregate : IAggregateRoot
{
    Task<TAggregate> FindByIdAsync(Guid id);

    Task SaveAsync(TAggregate aggregate);
}
