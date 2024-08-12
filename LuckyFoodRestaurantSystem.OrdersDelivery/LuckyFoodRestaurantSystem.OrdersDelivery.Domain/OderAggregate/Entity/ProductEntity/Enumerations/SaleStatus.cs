using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.Enumerations;

public partial class SaleStatus : Enumeration
{
    public static SaleStatus Available = new(1, "Доступен");

    public static SaleStatus UnAvailable = new(2, "Недоступен");
}