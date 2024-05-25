using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Rules;

public class PaymentStatusForOrderMustBePaid : IBusinessRule
{
    public PaymentStatusForOrderMustBePaid(PaymentStatus paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }

    public PaymentStatus PaymentStatus { get; private set; }

    public string ErrorMessage => "Невозможно назначить курьера на неоплаченный заказ";

    public bool IsBroken()
    {
        return PaymentStatus == PaymentStatus.Unpaid;
    }
}
