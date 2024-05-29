using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;


namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Rules;

public class OrderMayBeCanceledOnlyWithStatusAccepted : IBusinessRule
{
    public OrderMayBeCanceledOnlyWithStatusAccepted(OrderStatus orderStatus)
    {
        if (orderStatus != OrderStatus.Accepted)
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
