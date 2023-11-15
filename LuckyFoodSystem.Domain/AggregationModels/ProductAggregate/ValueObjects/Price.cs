using BuberDinner.Domain.Common.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class Price : ValueObject
    {
        public Price(float value)
        {
            if(value <= 0)
            {
                throw new Exception("Invalid input price");
            }
            Value = value;
        }
        public float Value { get; private set; }       
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
