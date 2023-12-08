using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.ReportAggregate
{
    public class Grade : ValueObject
    {
        public Grade(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Invalid input grade");

            Value = value;
        }
        public int Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}