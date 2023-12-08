using LuckyFoodSystem.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string value)
        {
            string pattern = @"^[a-zA-Z0-9a-яА-Я\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/`-]+$";
            if (value.Length < 3 || value.Length > 50 || !Regex.IsMatch(value, pattern))
                throw new Exception("Invalid input title");

            Value = value;
        }
        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
