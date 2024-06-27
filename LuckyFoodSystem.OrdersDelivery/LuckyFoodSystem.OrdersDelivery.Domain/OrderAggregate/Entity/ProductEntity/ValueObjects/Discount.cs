using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Entity.ProductEntity.ValueObjects;

public partial class Discount : ValueObject
{
    public Discount(int ordersCount, decimal maxDiscount, decimal discountBase)
    {
        decimal discount = 0;

        if (ordersCount < 0)
            throw new BusinessException("Количество заказов не может быть меньше 0");

        if (ordersCount is 0)
            discount = 0;

        if (ordersCount >= 5)
            discount = 3;

        if (ordersCount >= 15)
            discount = 7;

        if (ordersCount >= 30)
            discount = 10;

        if (discountBase >= 10)
        {
            discount /= 3 + discountBase;
        }

        if (discount > maxDiscount)
            discount = maxDiscount;

        Value = discount;
    }

    public decimal Value { get; private set; }
}
