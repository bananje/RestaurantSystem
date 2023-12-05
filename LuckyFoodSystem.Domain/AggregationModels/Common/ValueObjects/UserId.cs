using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class UserId : ValueObject
    {
        public Guid Value { get; private set; }
        public UserId(Guid value) => Value = value;
        public static UserId CreateUnique() => new(Guid.NewGuid());
        public static UserId Create(Guid value) => new UserId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
