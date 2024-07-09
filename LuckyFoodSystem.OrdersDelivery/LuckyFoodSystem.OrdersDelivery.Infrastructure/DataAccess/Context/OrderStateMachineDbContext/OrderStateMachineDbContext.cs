using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext;

public class OrderStateMachineDbContext : SagaDbContext
{
    public OrderStateMachineDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override IEnumerable<ISagaClassMap> Configurations { get; }
}
