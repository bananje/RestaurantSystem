using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;


namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Rules;

public class OrderMayBeCanceledOnlyWithStatusAccepted : IBusinessRule
{
    public OrderMayBeCanceledOnlyWithStatusAccepted(OrderStatus orderStatus)
    {
        if (orderStatus != OrderStatus.Created)
        {
            IsInvalid = true;
        }
    }

    public bool IsInvalid = false;

    public string ErrorMessage => $"Невозможно отменить заказ, так как он принят в работу";

    public bool IsBroken()
    {
        return IsInvalid;
    }
}
