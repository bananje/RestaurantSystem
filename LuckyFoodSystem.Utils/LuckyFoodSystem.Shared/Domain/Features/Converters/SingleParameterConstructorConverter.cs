using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace LuckyFoodSystem.Shared.Domain.Features.Converters
{
    public class SingleParameterConstructorConverter<T> : JsonConverter where T : class
    {
        private readonly ConstructorInfo _constructor;

        public SingleParameterConstructorConverter()
        {
            // Получаем конструктор с одним параметром
            _constructor = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                    .FirstOrDefault(c => c.GetParameters().Length == 1);

            if (_constructor == null)
            {
                throw new InvalidOperationException($"Type {typeof(T)} does not have a constructor with a single parameter.");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JToken token = JToken.Load(reader);

            // Получаем значение из JSON и используем его для вызова конструктора
            var value = token.ToObject(_constructor.GetParameters()[0].ParameterType, serializer);
            return _constructor.Invoke(new[] { value });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // Получаем значение единственного параметра через свойство или метод
            var parameterValue = value.GetType().GetProperty("Value")?.GetValue(value) ??
                                 value.GetType().GetMethod("GetValue")?.Invoke(value, null);

            serializer.Serialize(writer, parameterValue);
        }
    }
}
