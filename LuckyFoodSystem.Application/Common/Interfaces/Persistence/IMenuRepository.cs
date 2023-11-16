using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken = default);
        Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default);
        Task AddMenuAsync(Menu menu, string rootPath);
        //Task RemoveMenuAsync(int menuId, string rootPath);
        //Task UpdateMenuAsync(int menuId, Menu menu, string rootPath);
    }
}
