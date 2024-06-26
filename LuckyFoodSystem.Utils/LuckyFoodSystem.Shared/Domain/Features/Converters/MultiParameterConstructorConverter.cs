using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace LuckyFoodSystem.Shared.Domain.Features.Converters;

public class MultiParameterConstructorConverter<T> : JsonConverter where T : class
{
    private readonly ConstructorInfo _constructor;
    private readonly ParameterInfo[] _parameters;

    public MultiParameterConstructorConverter()
    {
        // Получаем конструктор с несколькими параметрами
        _constructor = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .FirstOrDefault()!;

        if (_constructor == null)
        {
            throw new InvalidOperationException($"Type {typeof(T)} does not have a constructor with multiple parameters.");
        }

        _parameters = _constructor.GetParameters();
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

        // Проверяем, является ли JSON объектом
        if (token.Type != JTokenType.Object)
        {
            throw new JsonSerializationException($"Expected JSON object but got {token.Type}.");
        }

        // Считываем значения параметров из JSON
        var values = _parameters.Select(p => token[p.Name]?.ToObject(p.ParameterType, serializer)).ToArray();

        return _constructor.Invoke(values);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var jObject = new JObject();

        foreach (var parameter in _parameters)
        {
            var propertyName = parameter.Name;

            // Попробуем найти свойство с такой же чувствительностью к регистру, что и в конструкторе
            var property = value.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            var field = value.GetType().GetField(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);

            if (property != null)
            {
                var propertyValue = property.GetValue(value);
                jObject.Add(propertyName, JToken.FromObject(propertyValue, serializer));
            }
            else if (field != null)
            {
                var fieldValue = field.GetValue(value);
                jObject.Add(propertyName, JToken.FromObject(fieldValue, serializer));
            }
            else
            {
                throw new InvalidOperationException($"Property or field '{propertyName}' not found on type '{typeof(T)}'.");
            }
        }

        jObject.WriteTo(writer);
    }
}
