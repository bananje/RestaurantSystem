using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate
{
    public class TotalPrice : ValueObject
    {
        public TotalPrice(float value) 
        {
            if(value <= 0)
                throw new Exception("Invalid input value");

            Value = value;
        }
        public float Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}