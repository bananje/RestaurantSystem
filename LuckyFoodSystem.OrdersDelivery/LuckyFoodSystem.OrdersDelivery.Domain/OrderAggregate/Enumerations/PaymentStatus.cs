using LuckyFoodSystem.Shared.Domain.Models;


namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;

public partial class PaymentStatus : Enumeration
{
    public static PaymentStatus Paid = new(1, nameof(Paid));

    public static PaymentStatus Unpaid = new(2, nameof(Unpaid));
}
