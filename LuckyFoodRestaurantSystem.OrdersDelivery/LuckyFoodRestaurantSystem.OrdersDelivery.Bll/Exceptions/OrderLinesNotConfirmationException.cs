namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Exceptions;

public class OrderLinesNotConfirmedException : Exception
{
    public OrderLinesNotConfirmedException(Guid orderId, string reason) 
        : base($"Отказ в подтверждении позиций для заказа {orderId}. Причина: {reason}")
    {

    } 
}
