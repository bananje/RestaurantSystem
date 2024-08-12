using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Exceptions;

public class OrderCancelledException(string message) : BusinessException(message);
