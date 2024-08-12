using ErrorOr;
using System.Net;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Features;

public partial class Errors
{
    public static class Order
    {
        public static Error ConfirmationOrderLinesError(string message)
            => Error.Failure(code: HttpStatusCode.Conflict.ToString(), message);

    }

    public static class Courier
    {
        public static Error CourierNotFount(string courierId)
          => Error.Failure(code: HttpStatusCode.NotFound.ToString(), $"Курьер с ID:{courierId} не найден!");
    }

    public static class Common
    {
        public static Error BusinessFailure(string message)
            => Error.Failure(code: "BusinessFailure", message);
    }

    public static class Customer
    {
        public static Error NotFound(string message)
            => Error.Failure(code: HttpStatusCode.NotFound.ToString(), message);
    }
}

