using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.ValueObjects;

public partial class ShortDescription : ValueObject
{
    public ShortDescription(string value)
    {
        if (value.Length > 75 || value.Length < 0)
            throw new BusinessException("Допустимая длина короткого описания продукта от 1 до 75 символов");

        Value = value;
    }

    public string Value { get; private set; }
}
