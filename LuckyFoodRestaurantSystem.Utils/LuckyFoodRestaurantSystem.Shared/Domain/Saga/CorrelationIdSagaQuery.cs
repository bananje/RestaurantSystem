using MassTransit;
using System.Linq.Expressions;

namespace LuckyFoodRestaurantSystem.Shared.Domain.Saga;

public class CorrelationIdSagaQuery<TSaga> : ISagaQuery<TSaga> where TSaga : class, ISaga
{
    private readonly Guid _correlationId;

    public CorrelationIdSagaQuery(Guid correlationId)
    {
        _correlationId = correlationId;
    }

    public Expression<Func<TSaga, bool>> FilterExpression => saga => saga.CorrelationId == _correlationId;

    public Func<TSaga, bool> GetFilter()
    {
        return FilterExpression.Compile();
    }
}
