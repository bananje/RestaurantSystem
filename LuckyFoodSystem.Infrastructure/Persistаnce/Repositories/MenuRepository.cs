using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using LuckyFoodSystem.Infrastructure.Services.MemoryCacheService;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IMemoryCacheProvider _memoryCacheProvider;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IImageService _imageService;
        private readonly LuckyFoodDbContext _context;

        public MenuRepository(IMemoryCacheProvider memoryCacheProvider,
                              IHttpContextProvider httpContextProvider,
                              IImageService imageService,
                              LuckyFoodDbContext context)
        {
            _memoryCacheProvider = memoryCacheProvider;
            _httpContextProvider = httpContextProvider;
            _imageService = imageService;
            _context = context;
        }
        
        public async Task AddMenuAsync(Menu menu, string rootPath)
        {
            if (menu is not null)
            {               
                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;
                List<Image> images = await _imageService.LoadImage(files, rootPath);

                try
                {
                    images.ForEach(async img => { await _context.Images.AddAsync(img); });
                    menu.AddImage(images);
                    _context.Menus.Add(menu);
                    _context.SaveChanges();

                    _memoryCacheProvider.Set(CacheSettings<Menu>.ObjKey + menu.Id.Value,
                                             menu,
                                             TimeSpan.FromHours(CacheSettings<Menu>.StorageTime));

                    List<Menu> cachedMenus = new();
                    cachedMenus = _memoryCacheProvider
                                                .Get<List<Menu>>(CacheSettings<Menu>.ListObjKey);   
                    if(cachedMenus is null)
                    {
                        List<Menu> addToCacheMenus = new() { menu };
                        _memoryCacheProvider.Set(CacheSettings<List<Menu>>.ListObjKey,
                                             cachedMenus,
                                             TimeSpan.FromHours(CacheSettings<Menu>.StorageTime));
                    }                   
                }
                finally
                {
                    await Task.CompletedTask;
                }                
            }
        }

        public async Task<Menu> GetMenuByIdAsync(MenuId menuId)
        {
            await Task.CompletedTask;

            Menu? menu = new();
            var cachedMenu = _memoryCacheProvider
                                        .Get<Menu>(CacheSettings<Menu>.ObjKey + menuId);

            if(cachedMenu is null)
            {
                menu = _context.Menus.AsEnumerable().SingleOrDefault(u => u.Id.Value == menuId.Value);
                _memoryCacheProvider.Set<Menu>(CacheSettings<Menu>.ObjKey,
                                               menu,
                                               TimeSpan.FromHours(CacheSettings<Menu>.StorageTime));
            }
            else
            {
                menu = cachedMenu;
            }

            return null;
        }

        public async Task<List<Menu>> GetMenusAsync()
        {
            await Task.CompletedTask;

            var menus = new List<Menu>();
            var cachedMenus = _memoryCacheProvider
                                     .Get<List<Menu>>(CacheSettings<Menu>.ListObjKey);
            if(cachedMenus is null || cachedMenus.Count() is 0)
            {
                menus = await _context.Menus.ToListAsync();
                _memoryCacheProvider.Set(CacheSettings<List<Menu>>.ListObjKey,
                                         menus,
                                         TimeSpan.FromHours(CacheSettings<Menu>.StorageTime));
            }
            else
            {
                menus = cachedMenus;
            }
            
            return menus;
        }

        public async Task<List<Menu>> GetMenusByCategoryAsync(int categoryId)
        {
            await Task.CompletedTask;

            List<Menu> selectedMenus = new();
            var cachedMenus = _memoryCacheProvider
                                    .Get<List<Menu>>(CacheSettings<Menu>.ListObjKey);

            if(cachedMenus is not null)
            {
                selectedMenus = cachedMenus
                                    .Where(u => u.Category.Id == categoryId).ToList();
            }
            else
            {
                selectedMenus = await _context.Menus
                        .Where(u => u.Category == Category.FromId(categoryId)).ToListAsync();

                _memoryCacheProvider.Set(CacheSettings<List<Menu>>.ListObjKey,
                                         selectedMenus,
                                         TimeSpan.FromHours(CacheSettings<Menu>.StorageTime));
            }
            
            return selectedMenus;
        }
    }
}
