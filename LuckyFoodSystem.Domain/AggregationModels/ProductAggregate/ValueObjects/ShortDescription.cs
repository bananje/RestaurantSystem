using BuberDinner.Domain.Common.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.ProductAggregate.ValueObjects
{
    public class ShortDescription : ValueObject
    {
        public ShortDescription(string value)
        {
            if (value.Length < 7 || value.Length > 50)
            {
                throw new Exception("Invalid input shortDescription");
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
