namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Exceptions;

public class CustomerDataNotFoundException : Exception
{
    public CustomerDataNotFoundException(Guid customerId, string reason)
        : base($"Ошибка получения данных заказчика ID:{customerId}. Причина: {reason}")
    {

    }
}
