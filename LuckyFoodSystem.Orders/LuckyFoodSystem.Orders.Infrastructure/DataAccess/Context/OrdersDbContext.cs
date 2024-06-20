using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Courier;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Customer;
using LuckyFoodSystem.Orders.Infrastructure.DataAccess.Configurations.Order;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Context;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {       
    }

    public DbSet<OrderInfo> Orders { get; set; }

    public DbSet<CustomerInfo> Customers { get; set; }

    public DbSet<CourierInfo> Couriers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.ApplyConfiguration(new OrderLineEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());

        modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());

        modelBuilder.ApplyConfiguration(new CourierEntityConfiguration());
    }
}
