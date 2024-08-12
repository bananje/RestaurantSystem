using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Rules;

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
