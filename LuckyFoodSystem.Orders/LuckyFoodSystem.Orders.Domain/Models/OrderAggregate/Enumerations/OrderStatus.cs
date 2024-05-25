using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;

public partial class OrderStatus : Enumeration
{
    public static OrderStatus Accepted = new (1, nameof(Accepted));

    public static OrderStatus InProcess = new(2, nameof(InProcess));

    public static OrderStatus Complete = new(3, nameof(Complete));

    public static OrderStatus HandedByCourier = new(4, nameof(HandedByCourier));

    public static OrderStatus Delivered = new(5, nameof(Delivered));

    public static OrderStatus Canceled = new(6, nameof(Canceled));
}
