using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace LuckyFoodSystem.Shared.Domain.Features.Converters;

public class MultiParameterConstructorConverter<T> : JsonConverter where T : class
{
    private readonly ConstructorInfo _constructor;
    private readonly ParameterInfo[] _constructorParameters;

    public MultiParameterConstructorConverter()
    {
        // Получаем конструктор с несколькими параметрами
        _constructor = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                .FirstOrDefault();

        if (_constructor == null)
        {
            throw new InvalidOperationException($"Type {typeof(T)} does not have a suitable constructor.");
        }

        _constructorParameters = _constructor.GetParameters();
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

        // Загружаем JSON объект
        JObject jsonObject = JObject.Load(reader);

        // Получаем значения для каждого параметра конструктора
        var constructorArguments = _constructorParameters
            .Select(p => jsonObject[p.Name]?.ToObject(p.ParameterType, serializer))
            .ToArray();

        // Вызываем конструктор с полученными значениями
        return _constructor.Invoke(constructorArguments);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        // Приводим объект к нужному типу
        var jsonObject = new JObject();

        // Записываем значения свойств в JSON объект
        foreach (var parameter in _constructorParameters)
        {
            var property = value.GetType().GetProperty(parameter.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null)
            {
                var propertyValue = property.GetValue(value);
                jsonObject.Add(parameter.Name, JToken.FromObject(propertyValue, serializer));
            }
        }

        // Сериализуем JSON объект
        jsonObject.WriteTo(writer);
    }
}
