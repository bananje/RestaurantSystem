using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Exceptions;

public class OrderCancelledException(string message) : BusinessException(message);
