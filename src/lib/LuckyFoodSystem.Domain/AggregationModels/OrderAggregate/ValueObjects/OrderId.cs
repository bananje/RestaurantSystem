using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class OrderId : ValueObject
    {
        public Guid Value { get; private set; }
        public OrderId(Guid value) => Value = value;
        public static OrderId CreateUnique() => new(Guid.NewGuid());
        public static OrderId Create(Guid value) => new OrderId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
