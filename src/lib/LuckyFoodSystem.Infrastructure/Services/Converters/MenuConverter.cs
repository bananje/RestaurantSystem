using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LuckyFoodSystem.Infrastructure.Services.Converters
{
    public class MenuConverter : JsonConverter<Menu>
    {
        public override Menu? ReadJson(JsonReader reader, Type objectType,
                                       Menu? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            var menu = Menu.Set(
                MenuId.Create(Guid.Parse(obj[nameof(Menu.Id)]!.ToString())),
                new Name(obj[nameof(Menu.Name)]!.ToString()),
                Category.FromName(obj[nameof(Menu.Category)]!.ToString()));

            var imagesToken = obj[nameof(Menu.Images)];
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
            var images = value!.Images.Select(image => new { image.Path, image.Id.Value }).ToList();

            JObject obj = new JObject
            {
                { nameof(Menu.Id), value.Id.Value },
                { nameof(Menu.Name), value.Name.Value },
                { nameof(Menu.Category), value.Category.Name },
                { nameof(Menu.Images), JToken.FromObject(images, serializer) }
            };

            obj.WriteTo(writer);
        }
    }
}
