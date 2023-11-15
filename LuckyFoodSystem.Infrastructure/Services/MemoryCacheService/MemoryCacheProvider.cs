using LuckyFoodSystem.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace LuckyFoodSystem.Infrastructure.Services.MemoryCacheService
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheProvider(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        public T Get<T>(string key) => _memoryCache.TryGetValue(key, out T value) ? value : default;
        public void Remove(string key) => _memoryCache.Remove(key);
        public void Set<T>(string key, T value, TimeSpan absoluteExpiration)
            => _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration
            });             
    }
}
