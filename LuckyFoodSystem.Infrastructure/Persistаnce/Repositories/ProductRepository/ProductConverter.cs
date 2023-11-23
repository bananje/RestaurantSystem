using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.Enumerations;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.ProductAggregate.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository
{
    internal class ProductConverter : JsonConverter<Product>
    {
        public override Product? ReadJson(JsonReader reader, Type objectType, Product? existingValue,
                                          bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            var product = Product.Set(
                ProductId.Create(Guid.Parse(obj[nameof(Product.Id)]!.ToString())),
                new Title(obj[nameof(Product.Title)]!.ToString()),
                new Price(float.Parse(obj[nameof(Product.Price)]!.ToString())),
                new Description(obj[nameof(Product.Description)]!.ToString()),
                new ShortDescription(obj[nameof(Product.ShortDescription)]!.ToString()),
                new Weight(float.Parse(obj[nameof(Product.Weight.WeightValue)]!.ToString()),
                           WeightUnits.FromName(obj[nameof(Product.Weight.WeightUnit)]!.ToString())),    
                Category.FromName(obj[nameof(Product.Category)]!.ToString()));

            var imagesToken = obj[nameof(Product.Images)];
            if (imagesToken != null && imagesToken.Type == JTokenType.Array)
            {
                List<Image> images = new();
                foreach (var imageToken in imagesToken.Children())
                {
                    var image = Image.Set(
                        ImageId.Create(Guid.Parse(imageToken["Value"]!.ToString())),
                        imageToken["Value"]!.ToString());

                    images.Add(image);
                }
                product.AddImages(images);
            }

            var menustoken = obj[nameof(Product.Menus)];
            if (menustoken != null && menustoken.Type == JTokenType.Array)
            {
                List<Menu> menus = new();
                foreach (var menuToken in menustoken.Children())
                {
                    var menu = Menu.Set(
                        MenuId.Create(Guid.Parse(menuToken["MenuId"]!.ToString())),
                        new Name(menuToken["MenuName"]!.ToString()),
                        Category.FromName(menuToken["MenuCategory"]!.ToString()));

                    menus.Add(menu);
                }
                product.AddMenus(menus);
            }

            return product;
        }

        public override void WriteJson(JsonWriter writer, Product? value, JsonSerializer serializer)
        {
            var images = value!.Images.Select(image => new { image.Path, image.Id.Value }).ToList();
            var menus = value!.Menus.Select(menu => new { MenuId = menu.Id.Value,
                                                          MenuName = menu.Name.Value,
                                                          MenuCategory = menu.Category.Name }).ToList();

            JObject obj = new JObject
            {
                { nameof(Product.Id), value.Id.Value },
                { nameof(Product.Title), value.Title.Value },
                { nameof(Product.Description), value.Description.Value },
                { nameof(Product.ShortDescription), value.ShortDescription.Value },
                { nameof(Product.Price), value.Price.Value },
                { nameof(Product.Weight.WeightValue), value.Weight.WeightValue },
                { nameof(Product.Weight.WeightUnit), value.Weight.WeightUnit.Name },
                { nameof(Product.Category), value.Category.Name },
                { nameof(Product.Images), JToken.FromObject(images, serializer) },
                { nameof(Product.Menus), JToken.FromObject(menus, serializer) }
            };

            obj.WriteTo(writer);
        }
    }
}
