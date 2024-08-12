using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Rules;

public class OrderStatusChangesMustBeConsistent : IBusinessRule
{
    public OrderStatusChangesMustBeConsistent(OrderStatus currentStatus, OrderStatus newStatus)
    {
        IsStatusChangeValid = newStatus.Id == currentStatus.Id + 1;
    }

    public bool IsStatusChangeValid { get; private set; }

    public string ErrorMessage => throw new NotImplementedException();

    public bool IsBroken()
    {
        return IsStatusChangeValid;
    }
}
