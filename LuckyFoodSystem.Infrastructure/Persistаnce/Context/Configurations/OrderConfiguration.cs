using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.OrderAggregate;
using LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => OrderId.Create(value));

            builder.OwnsOne(u => u.Number, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Number")
                    .HasMaxLength(15)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.UserName, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("UserName")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.PhoneNumber, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.Property(m => m.AdressId)
                .HasConversion(
                id => id.Value,
                value => AdressId.Create(value));

            builder.Property(m => m.CustomerId)
               .HasConversion(
               id => id.Value,
               value => UserId.Create(value));

            builder.Property(m => m.CourierId)
               .HasConversion(
               id => id.Value,
               value => UserId.Create(value));

            builder.OwnsOne(u => u.TotalPrice, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("TotalPrice")
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Discount, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Discount")
                    .IsRequired();
            });

            builder.Property(x => x.OrderStatus)
               .IsRequired()
               .HasConversion(
                   id => id.Id,
                   value => OrderStatus.FromId(value));

            builder.OwnsOne(u => u.Comment, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Comment")
                    .HasMaxLength(300)
                    .IsRequired();
            });

            builder.Property(x => x.OrderDate)
                .IsRequired();

            builder.Property(x => x.IsAutorize)
                .IsRequired();

            builder.Property(x => x.IsCompleted)
                .IsRequired();
        }
    }
}
