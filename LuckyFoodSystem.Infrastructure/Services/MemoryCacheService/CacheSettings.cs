namespace LuckyFoodSystem.Infrastructure.Services.MemoryCacheService
{
    public static class CacheSettings<T>
    {
        public const string ObjKey = $"{nameof(T)} obj with ID -";
        public const string ListObjKey = $"{nameof(T)} obj list";
        public const int StorageTime = 3;
    }
}
