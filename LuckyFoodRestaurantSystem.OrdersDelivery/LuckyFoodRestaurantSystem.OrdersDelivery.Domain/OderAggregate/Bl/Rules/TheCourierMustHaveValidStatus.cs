using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Rules;

public class TheCourierMustHaveValidStatus : IBusinessRule
{
    private List<CourierStatus> _invalidStatuses = new() { CourierStatus.Inactive, CourierStatus.Delivering };

    public TheCourierMustHaveValidStatus(CourierStatus courierStatus)
    {
        CourierStatus = courierStatus;
    }

    public CourierStatus CourierStatus { get; private set; }
    public string ErrorMessage => $"Невозможно назначить курьера cо статусом {CourierStatus.Name}";

    public bool IsBroken()
    {
        return _invalidStatuses.Contains(CourierStatus);
    }
}
