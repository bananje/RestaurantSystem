using LuckyFoodSystem.Domain.Models;
using Newtonsoft.Json.Linq;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class UserName : ValueObject
    {
        public UserName(string name)
        {
            if (name.Length < 3 || name.Length > 50)
            {
                throw new Exception("Invalid input user name");
            }
            Value = name;
        }
        public string Value { get; private set; }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
