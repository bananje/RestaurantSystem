using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class Comment : ValueObject
    {
        public Comment(string value) => Value = value;
        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}