using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Exceptions;

public class CourierAssignedException(string message) : BusinessException(message);

