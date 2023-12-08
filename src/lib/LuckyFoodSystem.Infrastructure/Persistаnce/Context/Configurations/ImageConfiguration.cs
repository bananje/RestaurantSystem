using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(c => c.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ImageId.Create(value));

            builder.Property(u => u.Path);          
        }
    }
}
