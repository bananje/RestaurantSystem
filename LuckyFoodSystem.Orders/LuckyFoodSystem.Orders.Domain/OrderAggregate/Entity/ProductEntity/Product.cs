using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.ProductEntity
{
    public class Product : Entity<ProductId>
    {
        public string Title { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public string ProductImageUrl { get; private set; } = null!;

        public SaleStatus Status { get; private set; } = null!;

        public ShortDescription ShortDescription { get; private set; } = null!;

        public Price Price { get; private set; } = null!;

        public Discount Discount { get; private set; } = null!;

        public decimal? BaseDiscount { get; private set; }

        public decimal? MaxDiscount { get; private set; }

        public decimal PriceWithDiscount => GetPriceWithDiscount(Price, Discount);

        public Weight Weight { get; private set; } = null!;

        public WeightUnit WeightUnit { get; private set; } = null!;

        public Product(string title,
                       string description,
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
            Description = description;
            ProductImageUrl = productImageUrl;
            ShortDescription = shortDescription;
            Price = price;
            Discount = discount;
            Weight = weight;
            WeightUnit = weightUnit;
        }

        decimal GetPriceWithDiscount(Price price, Discount discount)
        {
            return price.Value - price.Value * discount.Value / 100;
        }
    }
}
