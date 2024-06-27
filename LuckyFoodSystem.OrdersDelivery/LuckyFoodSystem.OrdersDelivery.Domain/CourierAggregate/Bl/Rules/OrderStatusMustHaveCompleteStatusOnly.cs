using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Rules;

public class OrderStatusMustHaveCompleteStatusOnly : IBusinessRule
{
    public OrderStatus OrderStatus { get; private set; }

    public OrderStatusMustHaveCompleteStatusOnly(OrderStatus currentStatus)
    {
        OrderStatus = currentStatus;
    }

    public string ErrorMessage => $"Курьер может брать заказ только со статусом {nameof(OrderStatus.Complete)}";

    public bool IsBroken()
    {
        return OrderStatus == OrderStatus.Complete ? false : true;
    }
}
