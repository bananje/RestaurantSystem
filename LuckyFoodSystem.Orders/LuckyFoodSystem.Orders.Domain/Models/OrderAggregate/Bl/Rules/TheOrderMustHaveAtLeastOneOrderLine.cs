using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Features;


namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Rules;

public class TheOrderMustHaveAtLeastOneOrderLine : IBusinessRule
{
    public TheOrderMustHaveAtLeastOneOrderLine(HashSet<OrderLine> orderLines)
    {
        OrderLines = orderLines;
    }

    public HashSet<OrderLine> OrderLines { get; private set; }

    public string ErrorMessage => "Заказ должен иметь хотя бы одну позицию";

    public bool IsBroken()
    {
        return OrderLines.Count > 0 ? false : true;
    }
}
