using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.ValueObjects;

public partial class Weight : ValueObject
{
    public Weight(double weightValue)
    {
        if (weightValue <= 0)
            throw new BusinessException("Вес продукта не может быть меньше 0");

        Value = weightValue;
    }

    public double Value { get; private set; }
}
