using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Entity.ProductEntity.ValueObjects;

public partial class Discount : ValueObject
{
    public Discount(decimal value)
    {
        if (value < 0)
            throw new BusinessException("Скидка не может быть отрицательной");

        Value = value;        
    }

    public decimal Value { get; private set; }
}
