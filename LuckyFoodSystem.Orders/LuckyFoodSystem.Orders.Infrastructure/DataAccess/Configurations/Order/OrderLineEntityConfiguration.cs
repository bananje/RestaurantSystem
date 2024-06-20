using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Order;

public class OrderLineEntityConfiguration : IEntityTypeConfiguration<OrderLineInfo>
{
    public void Configure(EntityTypeBuilder<OrderLineInfo> builder)
    {
        builder.HasKey(ol => ol.OrderLineId);

        builder.Property(ol => ol.ReadyStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ol => ol.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ol => ol.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Связь с ProductInfo
        builder.HasOne(ol => ol.Product)
            .WithMany()
            .HasForeignKey(nameof(ProductInfo.ProductId));

        builder.ToTable("OrderLines"); // Имя таблицы в базе данных
    }
}
