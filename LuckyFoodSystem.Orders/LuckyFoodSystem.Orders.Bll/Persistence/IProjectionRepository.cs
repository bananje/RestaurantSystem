namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IProjectionRepository<T> : IQueryRepository<T>
{
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
}
