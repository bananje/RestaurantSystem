using BuberDinner.Domain.Common.Models;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;

namespace LuckyFoodSystem.AggregationModels.MenuAggregate
{
    public class Menu : Entity<MenuId>
    {
        private readonly List<Product> _products = new();      
        private readonly List<Image> _images = new();
        public Name Name { get; private set; } = null!;
        public Category Category { get; private set; } = null!;
        public IReadOnlyList<Image> Images => _images.ToList();
        public IReadOnlyCollection<Product> Products => _products;
        private Menu(MenuId menuId,
                     Name name,
                     Category category) : base(menuId)
        {
            Name = name;
            Category = category;
        }
        public Menu() { }

        public static Menu Create(Name name, Category category)
            => new(MenuId.CreateUnique(), name, category);

        public void AddImage(List<Image> newImages)
            => newImages.ForEach(img => { _images.Add(img); });         
    }
}
