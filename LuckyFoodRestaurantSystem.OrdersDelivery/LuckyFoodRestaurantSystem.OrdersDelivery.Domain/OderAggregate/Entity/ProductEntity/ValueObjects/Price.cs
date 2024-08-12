using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.ValueObjects;

public partial class Price : ValueObject
{
    public Price(decimal value)
    {
        if (value < 0)
            throw new BusinessException("Цена не может быть отрицательной");

        Value = value;
    }

    public decimal Value { get; private set; }
}
