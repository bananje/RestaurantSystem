using ErrorOr;
using System.Net;

namespace LuckyFoodSystem.Orders.Bll.Results;

public partial class Errors
{
    public static class Order
    {
        public static Error ProductNotFount(string message)
            => Error.Failure(code: HttpStatusCode.NotFound.ToString(), message);

        public static Error InvalidCustomer(string message)
            => Error.Failure(code: HttpStatusCode.NotFound.ToString(), message);

        public static Error OrderNotFount(string message)
           => Error.Failure(code: HttpStatusCode.NotFound.ToString(), message);
    }

    public static class Common
    {
        public static Error BusinessFailure(string message)
            => Error.Failure(code: "BusinessFailure", message);
    }

    public static class Customer
    {
        public static Error CustomerNotFound(string message)
            => Error.Failure(code: HttpStatusCode.NotFound.ToString(), message);
    }
}

