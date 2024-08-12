namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
