using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Domain.Models;

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
        public static Menu Update(Menu oldMenu, Menu newMenu)
        {
            var updatedProperties = newMenu.GetType().GetProperties();
            foreach (var property in updatedProperties)
            {
                var newValue = property.GetValue(newMenu);

                if (newValue is IReadOnlyCollection<object> || newValue is MenuId) continue;

                if (newValue is not null)
                {
                    var oldProperty = oldMenu.GetType().GetProperty(property.Name);
                    if (oldProperty is not null && oldProperty.CanWrite)
                    {
                        oldProperty.SetValue(oldMenu, newValue);
                    }
                }
            }

            return oldMenu;
        }     
        public static Menu Set(MenuId menuId, Name name, Category category)
            => new(menuId, name, category);
        public void AddImages(List<Image> newImages)
            => newImages.ForEach(img => { _images.Add(img); });
        public void RemoveImages(List<Guid> imageIds)
            => _images.RemoveAll(img => imageIds.Contains(img.Id.Value));             
    }
}
