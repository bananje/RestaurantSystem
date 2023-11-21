using BuberDinner.Domain.Common.Models;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.ProductAggregate.ValueObjects;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        private readonly List<Image> _images = new();
        private readonly List<Menu> _menus = new();
        public IReadOnlyCollection<Image> Images => _images;
        public IReadOnlyCollection<Menu> Menus => _menus;
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public ShortDescription ShortDescription { get; private set; }
        public Price Price { get; private set; }
        public Weight Weight { get; private set; }
        public Category Category { get; private set; }
        public Product() { }
        private Product(ProductId productId,
                        Title title,
                        Price price,
                        Description description,
                        ShortDescription shortDescription,
                        Weight weight,
                        Category category) : base(productId) 
        {
            Title = title;
            Description = description;
            ShortDescription = shortDescription;
            Weight = weight;
            Category = category;
            Price = price;
        }
        public static Product Create(Title title,
                                     Price price,
                                     Description description,
                                     ShortDescription shortDescription,
                                     Weight weight,
                                     Category category)

            => new(ProductId.CreateUnique(), title, price, description, shortDescription, weight, category);
        public static Product Set(ProductId productId,
                               Title title,
                               Price price,
                               Description description,
                               ShortDescription shortDescription,
                               Weight weight,
                               Category category)

           => new(productId ,title, price, description, shortDescription, weight, category);
        public static Product Update(Product oldProduct, Product newProduct)
        {
            var updatedProperties = newProduct.GetType().GetProperties();
            foreach (var property in updatedProperties)
            {
                var newValue = property.GetValue(newProduct);

                if (newValue is IReadOnlyCollection<object> || newValue is MenuId) continue;

                if (newValue is not null)
                {
                    var oldProperty = oldProduct.GetType().GetProperty(property.Name);
                    if (oldProperty is not null && oldProperty.CanWrite)
                    {
                        oldProperty.SetValue(oldProduct, newValue);
                    }
                }
            }

            return oldProduct;
        }
        public void AddImages(List<Image> newImages)
            => newImages.ForEach(img => { _images.Add(img); });
        public void RemoveImages(List<Guid> imageIds)
            => _images.RemoveAll(img => imageIds.Contains(img.Id.Value));

    }
}
