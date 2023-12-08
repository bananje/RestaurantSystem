using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects
{
    public class MenuId : ValueObject
    {
        public Guid Value { get; private set; }
        public MenuId(Guid value) => Value = value;
        public static MenuId CreateUnique() => new(Guid.NewGuid());
        public static MenuId Create(Guid value) => new MenuId(value);
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
