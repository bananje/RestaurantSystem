using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            ConfigureMenuTable(builder);
        }

        private void ConfigureMenuTable(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuId.Create(value));

            builder.OwnsOne(u => u.Name, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("MenuName")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.Property(x => x.Category)
                .IsRequired()
                .HasConversion(
                    id => id.Id,
                    value => Category.FromId(value));          
        }       
    }
}
