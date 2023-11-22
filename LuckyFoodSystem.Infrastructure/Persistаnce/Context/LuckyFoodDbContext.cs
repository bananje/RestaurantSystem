﻿using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Context
{
    public class LuckyFoodDbContext : DbContext
    {
        public LuckyFoodDbContext() { }
        public LuckyFoodDbContext(DbContextOptions<LuckyFoodDbContext> dbContextOptions) 
            : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Menu> Menus { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }      
    }
}
