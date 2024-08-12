using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;

public partial class OrderStatus : Enumeration
{
    public static OrderStatus Created = new(1, "Создан");

    public static OrderStatus AwaitConfirmation = new(2, "Ожидаем подтверждения");

    public static OrderStatus Confirmed = new(3, "Подтвержден");

    public static OrderStatus InProcess = new(4, "Готовится");

    public static OrderStatus Complete = new(5, "Готов к выдаче");

    public static OrderStatus HandedByCourier = new(6, "Передан курьеру");

    public static OrderStatus Delivered = new(7, "Доставлен");

    public static OrderStatus Canceled = new(8, "Отменён");

    public static OrderStatus Error = new(9, "Ошибка");
}
