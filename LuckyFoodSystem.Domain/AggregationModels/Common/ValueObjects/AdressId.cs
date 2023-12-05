using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class AdressId : ValueObject
    {
        public Guid Value { get; private set; }
        public AdressId(Guid value) => Value = value;
        public static AdressId CreateUnique() => new(Guid.NewGuid());
        public static AdressId Create(Guid value) => new AdressId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
