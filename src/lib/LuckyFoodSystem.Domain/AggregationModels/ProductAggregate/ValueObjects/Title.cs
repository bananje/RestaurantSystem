using LuckyFoodSystem.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class Title : ValueObject
    {
        public Title(string value)
        {
            string pattern = @"^[a-zA-Z0-9a-яА-Я\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/`-]+$";
            if (!Regex.IsMatch(value, pattern) || value.Length < 3 || value.Length > 50)
            {
                throw new Exception("Invalid input title");
            }
            Value = value;
        }
        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
