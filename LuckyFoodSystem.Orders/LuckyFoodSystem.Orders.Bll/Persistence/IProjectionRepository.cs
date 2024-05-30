namespace LuckyFoodSystem.Orders.Bll.Persistence;

public interface IProjectionRepository<T> : IQueryRepository<T>
{
    Task InsertAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Guid Id);
}
