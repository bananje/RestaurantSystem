using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken);
        Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken);
        Task AddMenuAsync(Menu menu, string rootPath, CancellationToken cancellationToken);
        Task<bool> RemoveMenuAsync(MenuId menuId, string rootPath, CancellationToken cancellationToken);
        Task<Menu> UpdateMenuAsync(MenuId menuId ,Menu updatedMenu, string rootPath, 
                                   CancellationToken cancellationToken, List<Guid> imageIds = null!);
    }
}
