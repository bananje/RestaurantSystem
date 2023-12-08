using LuckyFoodSystem.Domain.Models;
using System.Text;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class Number : ValueObject
    {
        public Number() { }
        public Number(int value)
        {
            if(value <= 0)
                throw new Exception("Invalid input value");

            // форматирование числа согласно БТ
            var formattedNumber = string.Format("{0:D8}", value);

            Value = formattedNumber;
        }
        public string Value { get; private set; }   
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }        
    }
}
