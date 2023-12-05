using LuckyFoodSystem.Domain.Models;
using System.Text;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects
{
    public class Number : ValueObject
    {
        public Number(int number)
        {
            if(number <= 0)
                throw new Exception("Invalid input number");
            
            // форматирование числа согласно БТ
            var formattedNumber = number.ToString("000000000");

            Value = formattedNumber;
        }
        public string Value { get; private set; }   
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }        
    }
}
