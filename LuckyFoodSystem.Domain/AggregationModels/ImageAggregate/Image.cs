using BuberDinner.Domain.Common.Models;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate;

namespace LuckyFoodSystem.AggregationModels.ImageAggregate
{
    public class Image : Entity<ImageId>
    {
        private readonly HashSet<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products;

        private readonly HashSet<Menu> _menu = new();
        public IReadOnlyCollection<Menu> Menus => _menu;

        public string Path { get; private set; }
        private Image(ImageId imageId, string path) 
            : base(imageId) => Path = path;
        public Image() { }
        public static Image Create(string extension)
            => new Image(ImageId.CreateUnique(), path: Guid.NewGuid() + extension);
    }
}
