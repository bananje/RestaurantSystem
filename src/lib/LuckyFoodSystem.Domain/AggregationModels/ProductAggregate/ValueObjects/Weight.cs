using LuckyFoodSystem.Domain.Models;
using LuckyFoodSystem.AggregationModels.ProductAggregate.Enumerations;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects
{
    public class Weight : ValueObject
    {
        public Weight()
        {
            
        }
        public float WeightValue { get; private set; }
        public WeightUnits WeightUnit { get; private set; }
        public Weight(float weightValue, WeightUnits weightUnit) 
        { 
            if(weightValue <= 0)
                throw new Exception("Invalid input weght value");

            if(weightUnit is null)
                throw new Exception("Invalid input weght unit");

            WeightUnit = weightUnit;
            WeightValue = weightValue;
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return WeightValue;
            yield return WeightUnit;
        }
    }
}