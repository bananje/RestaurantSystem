using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Configurations.Order;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderInfo>
{
    public void Configure(EntityTypeBuilder<OrderInfo> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.CurrentStatus)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.PaymentStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(o => o.DeliveryAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(o => o.ClosedWithStatus)
            .HasMaxLength(100);

        builder.Property(o => o.OrderStatusChangedAt)
            .IsRequired();

        builder.Property(o => o.Version)
            .IsRequired();

        // Связь с OrderLineInfo
        builder.HasMany(o => o.OrderLines)
            .WithOne()
            .HasForeignKey(ol => ol.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // При необходимости можно изменить поведение при удалении

        builder.ToTable("Orders"); // Имя таблицы в базе данных
    }
}

