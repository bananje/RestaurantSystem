namespace LuckyFoodSystem.Infrastructure.Services.MemoryCacheService
{    
    public class CacheSettings
    {
        public const string Redis = nameof(Redis);
        public const string ObjKey = "_obj_with_ID";
        public const int ExpireTime = 3;
    }
}
