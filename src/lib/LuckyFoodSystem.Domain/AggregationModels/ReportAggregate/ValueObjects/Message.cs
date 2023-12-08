using LuckyFoodSystem.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Domain.AggregationModels.ReportAggregate.ValueObjects
{
    public class Message : ValueObject
    {
        public Message(string value)
        {
            if (!Regex.IsMatch(value, ".{15,}"))
                throw new ArgumentException("Invalid input report message");

            Value = value;
        }

        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}