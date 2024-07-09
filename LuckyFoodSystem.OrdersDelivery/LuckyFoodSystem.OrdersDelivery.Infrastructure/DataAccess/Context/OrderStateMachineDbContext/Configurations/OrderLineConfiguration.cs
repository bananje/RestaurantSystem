using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasIndex(u => u.OrderLineId).IsUnique();
        builder.HasKey(u => u.OrderLineId);

        builder.Property(u => u.OrderLineId)
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(u => u.Quantity).IsRequired();

        builder.HasOne(u => u.Product)
            .WithMany(u => u.OrderLines)
            .HasForeignKey(u => u.ProductId);
    }
}
