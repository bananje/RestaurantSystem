using LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;

public class OrderLine
{
    public Guid OrderLineId { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string ReadyStatus { get; set; } = string.Empty;


    public Product Product { get; set; } = null!;

    public OrderState OrderState { get; set; } = null!;
}
