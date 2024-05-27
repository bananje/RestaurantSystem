using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.Enumerations;

public partial class WeightUnit : Enumeration
{
    public static WeightUnit Gram = new(1, "Грамм");

    public static WeightUnit Kilogram = new(2, "Килограмм");

    public static WeightUnit Liter = new(3, "Литр");

    public static WeightUnit Milliliter = new(4, "Миллилитр");
}
