using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IEventSourcingRepository<TAggregate> 
    where TAggregate : IAggregateRoot
{
    Task<TAggregate> FindByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task SaveAsync(
        TAggregate aggregate,
        CancellationToken cancellationToken = default
    );
}
