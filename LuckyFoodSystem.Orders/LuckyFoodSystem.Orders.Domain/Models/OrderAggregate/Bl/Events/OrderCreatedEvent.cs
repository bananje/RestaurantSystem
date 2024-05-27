using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class OrderCreatedEvent : DomainEvent, IOrderEvent
{
   public OrderStatus OrderStatus { get; private set; }

   public PaymentStatus PaymentStatus { get; private set; }

   public CustomerId CustomerId { get; private set; }

   public Address DeliveryAddress { get; private set; }

   public decimal TotalPrice { get; private set; }

    public OrderCreatedEvent(
       OrderId orderId,
       OrderStatus orderStatus,
       PaymentStatus paymentStatus,
       CustomerId customerId,
       Address deliveryAddress,
       decimal totalPrice)
   {
        AggregateId = orderId.Value;
        OrderStatus = orderStatus;
        PaymentStatus = paymentStatus;
        CustomerId = customerId;
        DeliveryAddress = deliveryAddress;
        TotalPrice = totalPrice;
   }
}
