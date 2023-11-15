using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigureProductsTable(builder);
        }    
        private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ProductId.Create(value));
           
            builder.OwnsOne(u => u.Description, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Description")
                    .HasMaxLength(300)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Title, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Title")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Price, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Price")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Weight);

            builder.Property(x => x.Category)
                .IsRequired()
                .HasConversion(
                    id => id.Id,
                    value => Category.FromId(value));          
        }
    }
}

