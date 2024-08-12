using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.Enumerations;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity
{
    public class Product : Entity<ProductId>
    {
        public string Title { get; private set; } = string.Empty;

        public string ProductImageUrl { get; private set; } = string.Empty;

        public SaleStatus Status { get; private set; } = null!;

        public ShortDescription ShortDescription { get; private set; } = null!;

        public Price Price { get; private set; } = null!;

        public Discount Discount { get; private set; }

        public decimal PriceWithDiscount => GetPriceWithDiscount(Price, Discount);

        public Weight Weight { get; private set; } = null!;

        public WeightUnit WeightUnit { get; private set; } = null!;

        public Product(string title,
                       string productImageUrl,
                       ShortDescription shortDescription,
                       SaleStatus status,
                       Price price,
                       Discount discount,
                       Weight weight,
                       WeightUnit weightUnit)
        {
            Id = ProductId.CreateUnique();
            Title = title;
            ProductImageUrl = productImageUrl;
            ShortDescription = shortDescription;
            Price = price;
            Discount = discount;
            Weight = weight;
            WeightUnit = weightUnit;
            Status = status;
        }

        decimal GetPriceWithDiscount(Price price, Discount discount)
        {
            return price.Value - price.Value * discount.Value / 100;
        }
    }
}
