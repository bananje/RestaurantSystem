using LuckyFoodSystem.Shared.Mediatr;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.Models;

public record OrderInfo : IQueryObject
{
    public Guid OrderId { get; set; }

    public string CurrentStatus { get; set; } = string.Empty;

    public string PaymentStatus { get; set; } = string.Empty;

    public bool IsCourierAssigned { get; set; } = false;

    public Guid CourierId { get; set; }

    public Guid CustomerId { get; set; }

    public decimal TotalPrice { get; set; } = 0;

    public string DeliveryAddress { get; set; } = null!;

    public bool IsClosed { get; set; } = false;

    public string ClosedWithStatus { get; set; } = string.Empty;

    public DateTime OrderStatusChangedAt { get; set; }

    public ICollection<OrderLineInfo> OrderLines { get; set; } = [];

    public long Version { get; set; }
}

public record OrderLineInfo
{
    public Guid OrderId { get; set; }

    public Guid OrderLineId { get; set; }

    public ProductInfo Product { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public string ReadyStatus { get; set; } = string.Empty;
}

public record ProductInfo
{
    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ProductImageUrl { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal? BaseDiscount { get; set; }

    public decimal? MaxDiscount { get; set; }

    public decimal PriceWithDiscount { get; set; }

    public double Weight { get; set; }

    public string WeightUnit { get; set; } = string.Empty;
}

