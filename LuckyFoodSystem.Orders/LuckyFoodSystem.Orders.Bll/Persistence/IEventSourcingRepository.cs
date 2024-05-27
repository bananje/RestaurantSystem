namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IEventSourcingRepository<TAggregate>
{
    Task<TAggregate> FindByIdAsync(Guid id);
}
