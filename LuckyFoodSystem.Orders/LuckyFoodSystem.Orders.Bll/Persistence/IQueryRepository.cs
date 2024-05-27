using System.Linq.Expressions;

namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IQueryRepository<TAggregate>
{
    Task<IEnumerable<TAggregate>> FindAllAsync();

    Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> predicate);

    Task<TAggregate> FindByIdAsync(Guid id);
}
