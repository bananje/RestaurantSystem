using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Order;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductInfo>
{
    public void Configure(EntityTypeBuilder<ProductInfo> builder)
    {
        builder.HasKey(p => p.ProductId);

        builder.Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(p => p.ProductImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.ShortDescription)
            .HasMaxLength(500);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.BaseDiscount)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.MaxDiscount)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.PriceWithDiscount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Weight)
            .HasColumnType("float")
            .IsRequired();

        builder.Property(p => p.WeightUnit)
            .HasMaxLength(20)
            .IsRequired();

        builder.ToTable("Products"); // Имя таблицы в базе данных
    }
}
