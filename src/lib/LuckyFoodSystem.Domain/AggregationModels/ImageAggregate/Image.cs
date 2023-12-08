using LuckyFoodSystem.Domain.Models;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Domain.AggregationModels.ReportAggregate;

namespace LuckyFoodSystem.AggregationModels.ImageAggregate
{
    public class Image : Entity<ImageId>
    {
        private readonly HashSet<Product> _products = new();
        private readonly HashSet<Menu> _menu = new();
        private readonly HashSet<Report> _reports = new();
        public IReadOnlyCollection<Product> Products => _products;
        public IReadOnlyCollection<Menu> Menus => _menu;
        public IReadOnlyCollection<Report> Reports => _reports;

        public string Path { get; private set; }

        private Image(ImageId imageId, string path) 
            : base(imageId) => Path = path;
        public Image() { }

        public static Image Create(string extension)
            => new Image(ImageId.CreateUnique(), path: Guid.NewGuid() + extension);
        public static Image Set(ImageId imageId ,string path)
            => new Image(imageId,path);
    }
}
