using Microsoft.Extensions.Caching.Memory;
using RestaurantMenu.Models.Models;
using RestaurantMenu.Utils.IServices;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Models.VM;
using RestaurantSystem.Models;
using RestaurantSystem.Models.Models;

namespace RestaurantMenu.Utils.Services
{
    public class MenuService : IMenuService
    {
        private readonly RestaurantSystemDbContext _db;
        private IMemoryCache _memoryCache;

        public MenuService(RestaurantSystemDbContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }
        public async Task<List<Menu>> GetMenuAsync(int? categoryId)
        {
            _memoryCache.TryGetValue(WC.MenuCacheKey + categoryId, out List<Menu?>? cachedMenus);
            if (cachedMenus == null)
            {
                cachedMenus = await _db.Menus.Include(u => u.Image).ToListAsync();
                _memoryCache.Set(WC.MenuCacheKey, cachedMenus, TimeSpan.FromDays(1));
            }
            return cachedMenus;
        }
    }
}
