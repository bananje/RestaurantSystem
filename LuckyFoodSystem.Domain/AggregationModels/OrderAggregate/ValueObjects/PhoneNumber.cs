using LuckyFoodSystem.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string telNumber)
        {
            string pattern = @"^\d{10}$";
            if (!Regex.IsMatch(telNumber, pattern))
                throw new Exception("Invalid input telnumber");

            Value = telNumber;
        }
        public string Value { get; private set; } = null!;
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}