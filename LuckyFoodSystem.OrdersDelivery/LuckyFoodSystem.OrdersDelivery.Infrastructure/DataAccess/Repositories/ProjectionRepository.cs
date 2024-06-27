using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Repositories;

public class ProjectionRepository<TAggregate> : IProjectionRepository<TAggregate>
    where TAggregate : class, IQueryObject
{
    private readonly OrdersDbContext _context;

    private readonly DbSet<TAggregate> _dbSet;

    public ProjectionRepository(OrdersDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TAggregate>();
    }

    public async Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TAggregate> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task InsertAsync(TAggregate entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TAggregate entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Attach(entity);
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }
}
