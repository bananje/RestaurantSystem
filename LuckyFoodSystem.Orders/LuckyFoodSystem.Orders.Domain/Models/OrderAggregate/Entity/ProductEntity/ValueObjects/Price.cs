using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.ValueObjects;

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
