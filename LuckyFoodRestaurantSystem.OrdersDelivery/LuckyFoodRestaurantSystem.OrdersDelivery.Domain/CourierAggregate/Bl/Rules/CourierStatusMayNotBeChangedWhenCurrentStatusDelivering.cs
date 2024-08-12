using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Rules;

public class CourierStatusMayNotBeChangedWhenCurrentStatusDelivering : IBusinessRule
{
    public CourierStatusMayNotBeChangedWhenCurrentStatusDelivering(CourierStatus currentStatus, CourierStatus newStatus)
    {
        if (currentStatus == CourierStatus.Delivering)
        {
            IsInvalidStatus = true;
        }
    }

    private bool IsInvalidStatus = false;

    public string ErrorMessage => $"Невозможно изменить статус курьера," +
                $" пока текущий статус: {nameof(CourierStatus.Delivering)}";

    public bool IsBroken()
    {
        return IsInvalidStatus;
    }
}
