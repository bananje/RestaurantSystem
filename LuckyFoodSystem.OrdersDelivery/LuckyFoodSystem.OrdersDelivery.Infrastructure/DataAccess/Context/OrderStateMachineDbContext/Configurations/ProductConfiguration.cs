using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(u => u.ProductId).IsUnique();
        builder.HasKey(u => u.ProductId);

        builder.Property(u => u.ProductId).IsRequired().ValueGeneratedNever();

        builder.Property(u => u.Title).HasMaxLength(64);

        builder.Property(u => u.ProductImageUrl);

        builder.Property(u => u.Status).HasMaxLength(64);

        builder.Property(u => u.Price);

        builder.Property(u => u.Discount);

        builder.Property(u => u.PriceWithDiscount);

        builder.Property(u => u.Weight);

        builder.Property(u => u.WeightUnit).HasMaxLength(64);
    }
}
