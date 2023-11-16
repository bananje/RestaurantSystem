using LuckyFoodSystem.Models;

namespace LuckyFoodSystem.AggregationModels.Common.Enumerations
{
    public class Category : Enumeration
    {
        public static Category Food = new(1, nameof(Food));
        public static Category Bar = new(2, nameof(Bar));
        public Category(int id, string name) : base(id, name) { }
        public static Category FromId(int id)
            => GetAll<Category>().FirstOrDefault(x => x.Id == id)!;
        public static Category FromName(string name)
            => GetAll<Category>().FirstOrDefault(x => x.Name == name)!;        
    }
}
