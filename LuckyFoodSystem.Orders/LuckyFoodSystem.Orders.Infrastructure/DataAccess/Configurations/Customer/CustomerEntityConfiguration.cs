using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LuckyFoodSystem.Orders.Bll.QueryModels;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Customer;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerInfo>
{
    public void Configure(EntityTypeBuilder<CustomerInfo> builder)
    {
        builder.HasKey(c => c.CustomerId);

        builder.Property(c => c.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.MiddleName)
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.OrdersCount)
            .IsRequired();

        builder.Property(c => c.DeliveryAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(c => c.Version)
            .IsRequired();

        // Связь с OrderInfo
        builder.HasMany(c => c.Orders)
            .WithOne()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict); // Измените поведение при удалении по необходимости

        builder.ToTable("Customers"); // Имя таблицы в базе данных
    }
}
