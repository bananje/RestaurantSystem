using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.ReportAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReportId.Create(value));

            builder.Property(u => u.UserId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            builder.OwnsOne(u => u.Message, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Message")
                    .HasMaxLength(300)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Grade, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("Grade")
                    .IsRequired();
            });

            builder.Property(u => u.WriteDate)
                .IsRequired();
        }
    }
}
