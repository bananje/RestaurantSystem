using BuberDinner.Domain.Common.Models;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using System.Text.Json.Serialization;

namespace LuckyFoodSystem.AggregationModels.ImageAggregate
{
    public class Image : Entity<ImageId>
    {
        [JsonIgnore]
        private readonly HashSet<Product> _products = new();

        [JsonIgnore]
        public IReadOnlyCollection<Product> Products => _products;

        [JsonIgnore]
        private readonly HashSet<Menu> _menu = new();

        [JsonIgnore]
        public IReadOnlyCollection<Menu> Menus => _menu;

        public string Path { get; private set; }
        private Image(ImageId imageId, string path) 
            : base(imageId) => Path = path;
        public Image() { }
        public static Image Create(string extension)
            => new Image(ImageId.CreateUnique(), path: Guid.NewGuid() + extension);
    }
}
