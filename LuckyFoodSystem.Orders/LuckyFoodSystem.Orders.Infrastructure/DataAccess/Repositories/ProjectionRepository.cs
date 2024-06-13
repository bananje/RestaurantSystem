using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Repositories;

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

    public async Task<IEnumerable<TAggregate>> FindAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TAggregate> FindAsync(Expression<Func<TAggregate, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task InsertAsync(TAggregate entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TAggregate entity)
    {
        _dbSet.Attach(entity);
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }
}
