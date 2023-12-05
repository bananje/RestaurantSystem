using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate
{
    public class TotalPrice : ValueObject
    {
        public TotalPrice(float price) 
        {
            if(price <= 0)
                throw new Exception("Invalid input price");

            Value = price;
        }
        public float Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}