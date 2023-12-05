using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class Discount : ValueObject
    {
        public Discount(bool isAuthorize, float price, int ordersCount)
        {
            float discount = 0;
            if (isAuthorize)
            {
                discount = GetDiscount(price, ordersCount);
            }

            Value = discount;
        }

        public float Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
        private float GetDiscount(float price, int ordersCount)
        {
            int discountPercentage = 0; 

            // расчёт процента по количеству заказов до
            if(ordersCount > 5)
                discountPercentage = 3;
            if(ordersCount > 10)
                discountPercentage = 5;
            if(ordersCount > 50)
                discountPercentage = 15;

            // расчёт процента по текущей сумме заказа
            if (price > 2000)
                discountPercentage = 5;

            // конечный расчёт скидки
            float discount = price * (discountPercentage / 100);
            return discount;
        }
    }
}