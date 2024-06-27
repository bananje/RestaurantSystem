using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Entity.ProductEntity.Enumerations;

public partial class SaleStatus : Enumeration
{
    public static SaleStatus Available = new(1, nameof(Available));

    public static SaleStatus UnAvailable = new(2, nameof(UnAvailable));
}
