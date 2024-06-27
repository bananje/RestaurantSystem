using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Rules;

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
