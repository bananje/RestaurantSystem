using LuckyFoodSystem.AggregationModels.MenuAggregate;

namespace LuckyFoodSystem.Application.Menus.Common
{
    public record MenuResult(
        List<Menu>? Menus = null);
}
