using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Domain.AggregationModels.Common.Entities;
using LuckyFoodSystem.Domain.AggregationModels.OrderAggregate;
using LuckyFoodSystem.Domain.AggregationModels.ReportAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Duende.IdentityServer.Models.IdentityResources;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context
{
    public class LuckyFoodDbContext : DbContext
    {
        public LuckyFoodDbContext(DbContextOptions<LuckyFoodDbContext> dbContextOptions) 
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuckyFoodDbContext).Assembly);

            modelBuilder.Model.GetEntityTypes()
                              .SelectMany(e => e.GetProperties())
                              .Where(e => e.IsPrimaryKey())
                              .ToList()
                              .ForEach(e => e.ValueGenerated = ValueGenerated.Never);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithMany(i => i.Products)
                .UsingEntity(j => j.ToTable("ProductImage"));

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Menus)
                .WithMany(i => i.Products)
                .UsingEntity(j => j.ToTable("ProductMenus"));

            modelBuilder.Entity<Menu>()
                .HasMany(p => p.Images)
                .WithMany(i => i.Menus)
                .UsingEntity(j => j.ToTable("MenuImage"));

            modelBuilder.Entity<Order>()
                .HasMany(p => p.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(p => p.ToTable("OrderProduct"));

            modelBuilder.Entity<Report>()
               .HasMany(p => p.Images)
               .WithMany(p => p.Reports)
               .UsingEntity(p => p.ToTable("ReportImage"));

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }      
    }
}
