using LuckyFoodSystem.Models;

namespace LuckyFoodSystem.AggregationModels.ProductAggregate.Enumerations
{
    public class WeightUnits : Enumeration
    {
        public static WeightUnits Gram = new(1, "Грамм");
        public static WeightUnits Kilogram = new(2, "Килограмм");
        public static WeightUnits Liter = new(3, "Литр");
        public static WeightUnits Milliliter = new(4, "Миллилитр");

        public WeightUnits(int id, string name) : base(id, name) { }
        public static WeightUnits FromId(int id)
            => GetAll<WeightUnits>().FirstOrDefault(x => x.Id == id)!;
        public static WeightUnits FromName(string name)
            => GetAll<WeightUnits>().FirstOrDefault(x => x.Name == name)!;
    }
}
