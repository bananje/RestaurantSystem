using LuckyFoodSystem.Shared.Domain.Models;


namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;

public partial class PaymentStatus : Enumeration
{
    public static PaymentStatus Paid = new(1, nameof(Paid));

    public static PaymentStatus Unpaid = new(2, nameof(Unpaid));
}
