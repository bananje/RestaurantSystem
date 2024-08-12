using System.Linq.Expressions;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;

public interface IQueryRepository<TAggregate>
{
    Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TAggregate> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);
}
