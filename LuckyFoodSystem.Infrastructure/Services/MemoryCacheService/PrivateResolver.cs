using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace LuckyFoodSystem.Infrastructure.Services.MemoryCacheService
{
    public class PrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member,
                                                       MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if(!property.Writable)
            {
                var prop= member as PropertyInfo;

                bool hasPrivateSetter = prop?.GetGetMethod(true) != null;

                property.Writable = hasPrivateSetter;
            }

            return property;
        }
    }
}
