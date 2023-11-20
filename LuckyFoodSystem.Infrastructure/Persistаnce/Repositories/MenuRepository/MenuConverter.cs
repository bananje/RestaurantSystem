using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository
{
    public class MenuConverter : JsonConverter<Menu>
    {
        public override Menu? ReadJson(JsonReader reader, Type objectType, Menu? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            var menu = Menu.Set(
                MenuId.Create(Guid.Parse(obj["Id"]!.ToString())),
                new Name(obj["Name"]!.ToString()),
                Category.FromName(obj["Category"]!.ToString()));

            var imagesToken = obj["Images"];
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
                menu.AddImages(images);
            }

            return menu;
        }

        public override void WriteJson(JsonWriter writer, Menu? value, JsonSerializer serializer)
        {
            var images = value.Images.Select(image => new { image.Path, image.Id.Value }).ToList();

            //var properties = value.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    var propertyValue = property.GetValue(value);
            //    if (propertyValue is null) continue;

            //    JsonObject.Create();
            //}
            JObject obj = new JObject
            {
                { "Id", value.Id.Value },
                { "Name", value.Name.Value },
                { "Category", value.Category.Name },
                { "Images", JToken.FromObject(images, serializer) }
            };           

            obj.WriteTo(writer);
        }
    }
}
