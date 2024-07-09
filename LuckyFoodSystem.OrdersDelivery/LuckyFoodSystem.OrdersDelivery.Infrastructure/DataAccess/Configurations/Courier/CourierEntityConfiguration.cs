using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Configurations.Courier;

public class CourierEntityConfiguration : IEntityTypeConfiguration<CourierInfo>
{
    public void Configure(EntityTypeBuilder<CourierInfo> builder)
    {
        builder.HasKey(u => u.CourierId);

        builder.Property(c => c.FirstName)
            .HasMaxLength(100)
            .IsRequired(); // Предполагается, что Email является уникальным ключом. Можно изменить на Guid или другой идентификатор при необходимости.

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

        builder.Property(c => c.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Version)
            .IsRequired();

        // Отношение с OrderInfo, если необходимо
        builder.HasMany<OrderInfo>()
            .WithOne()
            .HasForeignKey(o => o.CourierId)
            .OnDelete(DeleteBehavior.Restrict); // Измените поведение при удалении по необходимости

        builder.ToTable("Couriers"); // Имя таблицы в базе данных
    }
}
