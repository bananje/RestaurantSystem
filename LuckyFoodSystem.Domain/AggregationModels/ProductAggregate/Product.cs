using BuberDinner.Domain.Common.Models;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        private readonly HashSet<Image> _images = new();
        private readonly HashSet<Menu> _menus = new();
        public IReadOnlyCollection<Image> Images => _images;
        public IReadOnlyCollection<Menu> Menus => _menus;
        public Title Title { get; private set; } = null!;
        public Description Description { get; private set; } = null!;
        public Price Price { get; private set; } = null!;
        public Weight Weight { get; private set; } = null!;
        public Category Category { get; private set; }
        public Product() { }
        private Product(ProductId productId,
                        Title title,
                        Price price,
                        Description description,
                        Weight weight,
                        Category category) : base(productId) 
        {
            Title = title;
            Description = description;
            Weight = weight;
            Category = category;
            Price = price;
        }
    }
}
