using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class ReportId : ValueObject
    {
        public Guid Value { get; private set; }
        public ReportId(Guid value) => Value = value;
        public static ReportId CreateUnique() => new(Guid.NewGuid());
        public static ReportId Create(Guid value) => new ReportId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
