using LuckyFoodSystem.Orders.Domain.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Context;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {       
    }

    public DbSet<Order> Orders {  get; set; }
    
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Courier> Couriers { get; set; }
}
