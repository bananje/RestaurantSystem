using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class UserName : ValueObject
    {
        public UserName(string value)
        {
            if (value.Length < 3 || value.Length > 50)
            {
                throw new Exception("Invalid input user value");
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
