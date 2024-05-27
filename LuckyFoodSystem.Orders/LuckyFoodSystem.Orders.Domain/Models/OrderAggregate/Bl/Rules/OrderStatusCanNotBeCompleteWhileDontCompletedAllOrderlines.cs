using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Rules;

public class OrderStatusCanNotBeCompleteWhileDontCompletedAllOrderlines : IBusinessRule
{
    public OrderStatusCanNotBeCompleteWhileDontCompletedAllOrderlines(IReadOnlyCollection<OrderLine> orderLines)
    {
        foreach (var orderLine in orderLines)
        {
            if (orderLine.ReadyStatus == ReadyStatus.Unready)
            {
                IsBrokenMarker = true;
                break;
            }
        }
    }

    private bool IsBrokenMarker = false;

    public string ErrorMessage => $"Невозможно изменить статус заказа на {nameof(OrderStatus.Complete)}" +
        $"пока статус всех позиций заказа не будет {nameof(ReadyStatus.Ready)}";

    public bool IsBroken()
    {
        return IsBrokenMarker;
    }
}
