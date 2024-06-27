using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;

public partial class CourierStatus : Enumeration
{
    public static CourierStatus Inactive = new(1, nameof(Inactive));

    public static CourierStatus Free = new(2, nameof(Free));

    public static CourierStatus Delivering = new(3, nameof(Delivering));
}
