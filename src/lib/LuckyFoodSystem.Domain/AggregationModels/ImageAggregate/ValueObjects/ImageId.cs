using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects
{
    public class ImageId : ValueObject
    {
        public Guid Value { get; private set; }
        public ImageId(Guid value) => Value = value;
        public static ImageId CreateUnique() => new(Guid.NewGuid());
        public static ImageId Create(Guid value) => new ImageId(value);        
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
