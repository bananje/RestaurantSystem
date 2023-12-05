using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class ProductId : ValueObject
    {
        public Guid Value { get; private set; }
        public ProductId(Guid value) => Value = value;
        public static ProductId CreateUnique() => new(Guid.NewGuid());
        public static ProductId Create(Guid value) => new ProductId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
