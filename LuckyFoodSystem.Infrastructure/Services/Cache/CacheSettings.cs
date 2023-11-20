namespace LuckyFoodSystem.Infrastructure.Services.Cache
{
    public class CacheSettings
    {
        public const string Redis = nameof(Redis);
        public const string ObjKey = "_obj_with_ID";
        public const int ExpireTime = 3;
    }
}
