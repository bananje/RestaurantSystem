namespace LuckyFoodSystem.Application.Common.Interfaces.Services
{
    public interface IRedisCacheService
    {
        Task<T> GetCollectionAsync<T>(CancellationToken cancellationToken, int categoryId = 0);
        Task SetCollectionAsync<T>(T obj, CancellationToken cancellationToken);

    }
}
