using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;

public partial class ReadyStatus : Enumeration
{
    public static ReadyStatus Ready = new(1, nameof(Ready));

    public static ReadyStatus Unready = new(2, nameof(Unready));
}
