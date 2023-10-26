using RestaurantMenu.Models.Models;
using RestaurantSystem.Models.Models;

namespace RestaurantMenu.Utils.IServices
{
    public interface IMenuService
    {
        Task<List<Menu>> GetMenuAsync(int? categoryId);
    }
}