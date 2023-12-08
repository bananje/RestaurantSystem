using LuckyFoodSystem.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string value)
        {
            string pattern = @"^\d{10}$";
            if (!Regex.IsMatch(value, pattern))
                throw new Exception("Invalid input telnumber");

            Value = value;
        }
        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}