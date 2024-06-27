using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Exceptions;

public class CourierAssignedException(string message) : BusinessException(message);

