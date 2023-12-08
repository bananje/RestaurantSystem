using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class AdressConfiguration : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.ToTable("Adresses");

            builder.HasKey(c => c.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => AdressId.Create(value));

            builder.Property<string>("Street")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property<string>("City")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property<string>("ApartmentNum")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property<string>("House")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property<string>("UserId");
        }
    }
}
