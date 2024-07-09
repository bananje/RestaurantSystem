using LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Configurations;

public class OrderStateMap : SagaClassMap<OrderState>
{
    protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);

        entity.Property(x => x.IsClosed);
        entity.Property(x => x.ClosedWithStatus).HasMaxLength(64); ;
        entity.Property(x => x.OrderStatusChangedAt);
        entity.Property(x => x.City).HasMaxLength(64);
        entity.Property(x => x.Street).HasMaxLength(64);
        entity.Property(x => x.House).HasMaxLength(64);
        entity.Property(x => x.ApartmentNum).HasMaxLength(64);

        entity.Property(x => x.TotalPrice);

        entity.Property(x => x.PaymentStatus).HasMaxLength(64);

        entity.HasMany(x => x.OrderLines)
            .WithOne(c => c.OrderState)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.Property(x => x.RowVersion).IsRowVersion();
    }
}
