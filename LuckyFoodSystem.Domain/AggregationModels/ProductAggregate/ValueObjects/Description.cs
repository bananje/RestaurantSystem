using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class Description : ValueObject
    {
        public Description(string value)
        {
            if(value.Length < 15 || value.Length > 300)
            {
                throw new Exception("Invalid input description");
            }
            Value = value;
        }
        public string Value { get; private set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}