using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

public class DeliveredOrderProjection(
    IProjectionRepository<OrderInfo> orderRepository,
    IEventSourcingRepository<Order> orderWriteRepository,
    IEventSourcingRepository<Courier> courierRepository,
    IEventSourcingRepository<Customer> customerRepository)
    : INotificationHandler<OrderDeliveredEvent>
{
    // TO-DO переделать под сагу
    public async Task Handle(OrderDeliveredEvent notification, CancellationToken cancellationToken)
    {
        //OrderInfo orderInfo = await orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value, cancellationToken);


        //if (orderInfo is not null)
        //{
        //    order.Version = notification.AggregateVersion;
        //    order.CurrentStatus = notification.OrderStatus.Name;

        //    await orderRepository.UpdateAsync(order);
        //}

        //Courier courier = await courierRepository.FindByIdAsync(notification.CourierId.Value, cancellationToken);

        //if (courier is not null)
        //{
        //    courier.AddCompletedOrder(notification.OrderId);

        //    await courierRepository.SaveAsync(courier, cancellationToken);
        //}

        //Customer customer = await customerRepository.FindByIdAsync(notification.CustomerId.Value, cancellationToken);

        //if (customer is not null)
        //{
        //    customer.AddOrder(order);
        //}
    }
}
