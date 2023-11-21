using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository
{
    internal class ProductConverter : JsonConverter<Product>
    {
        public override Product? ReadJson(JsonReader reader, Type objectType, Product? existingValue, bool hasExistingValue, JsonSerializer serializer)
        { 
           throw new NotImplementedException();
        //JObject obj = JObject.Load(reader);

        //var product = Product.Set(
        //    ProductId.Create(Guid.Parse(obj["Id"]!.ToString())),
        //    new Title(obj["Name"]!.ToString()),
        //    new Description(obj["Description"]!.ToString()),
        //    new Price(float.Parse(obj["Price"]!.ToString())),
        //    new Weight()
        //    Category.FromName(obj["Category"]!.ToString()));

        //var imagesToken = obj["Images"];
        //if (imagesToken != null && imagesToken.Type == JTokenType.Array)
        //{
        //    List<Image> images = new();
        //    foreach (var imageToken in imagesToken.Children())
        //    {
        //        var image = Image.Set(
        //            ImageId.Create(Guid.Parse(imageToken["Value"]!.ToString())),
        //            imageToken["Value"]!.ToString());

        //        images.Add(image);
        //    }
        //    product.AddImages(images);
        //}
        }

        public override void WriteJson(JsonWriter writer, Product? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
