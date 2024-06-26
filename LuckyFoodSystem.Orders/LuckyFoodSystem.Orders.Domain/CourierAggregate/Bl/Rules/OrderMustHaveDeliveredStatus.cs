using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Rules;

public class OrderMustHaveDeliveredStatus : IBusinessRule
{
    public OrderMustHaveDeliveredStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
    public OrderStatus OrderStatus { get; private set; }

    public string ErrorMessage => "Невозможно добавить заказ к выполненным. Статус заказа не имеет значение: Delivered";

    public bool IsBroken()
    {
        return OrderStatus != OrderStatus.Delivered ? true : false;
    }
}
