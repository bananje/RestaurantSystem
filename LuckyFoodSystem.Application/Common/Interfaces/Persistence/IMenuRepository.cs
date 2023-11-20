using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken = default);
        Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default);
        Task AddMenuAsync(Menu menu, string rootPath, CancellationToken cancellationToken = default);
        Task<bool> RemoveMenuAsync(MenuId menuId, string rootPath, CancellationToken cancellationToken = default);
        Task<Menu> UpdateMenuAsync(MenuId menuId ,Menu updatedMenu, string rootPath, 
                                   CancellationToken cancellationToken = default, List<Guid> imageIds = null!);
    }
}
