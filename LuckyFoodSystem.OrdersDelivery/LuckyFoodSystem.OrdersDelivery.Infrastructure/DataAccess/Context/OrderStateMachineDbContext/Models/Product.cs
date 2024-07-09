namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;

public class Product
{
    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ProductImageUrl { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal PriceWithDiscount { get; set; }

    public double Weight { get; set; }

    public string WeightUnit { get; set; } = string.Empty;


    public ICollection<OrderLine> OrderLines { get; set; } = [];
}
