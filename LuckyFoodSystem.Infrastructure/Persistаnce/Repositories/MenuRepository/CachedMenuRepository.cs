using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Infrastructure.Services.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository
{
    public class CachedMenuRepository : IMenuRepository
    {
        private readonly IMenuRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        private string _cacheKey = $"menu{CacheSettings.ObjKey}";

        public CachedMenuRepository(IMenuRepository decorated,
                                    IDistributedCache distributedCache,
                                    IConfiguration configuration)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
            _configuration = configuration;
        }        
        public async Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken)
        {
            string key = $"{_cacheKey}:{menuId.Value}";
            Menu? menu;

            string? cachedMenu = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedMenu))
            {
                menu = await _decorated.GetMenuByIdAsync(menuId, cancellationToken);

                if (menu is null) return menu!;

                await _distributedCache.SetStringAsync( 
                    key,
                    JsonConvert.SerializeObject(menu, new MenuConverter()),
                    cancellationToken);

                return menu;
            }

            menu = JsonConvert.DeserializeObject<Menu>(
                cachedMenu,
                new JsonSerializerSettings
                {
                    ConstructorHandling =
                        ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver(),
                    Converters = { new MenuConverter() }
                });

            return menu!;
        }              
        public async Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken)
        {           
            var menusList = await GetMenuCollectionFromCache(cancellationToken);

            if (menusList is null || menusList.Count() is 0)
            {
                menusList = await _decorated.GetMenusAsync(cancellationToken);
                await SetMenuCollectionToCache(menusList, cancellationToken);
            }

            return menusList;         
        }
        public async Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var menusByCategoryNameList = await GetMenuCollectionFromCache(cancellationToken, categoryId);
            
            if(menusByCategoryNameList is null || menusByCategoryNameList.Count() is 0)
            {
                menusByCategoryNameList = await _decorated.GetMenusByCategoryAsync(categoryId, cancellationToken);
                await SetMenuCollectionToCache(menusByCategoryNameList, cancellationToken);
            }

            return menusByCategoryNameList;
        }
        public async Task AddMenuAsync(Menu menu, string rootPath, CancellationToken cancellationToken)
        {
            if (menu is not null)
            {
                await _decorated.AddMenuAsync(menu, rootPath, cancellationToken);

                string key = $"{_cacheKey}:{menu.Id.Value}";
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(menu, new MenuConverter()),
                    cancellationToken);
            }
        }      
        public async Task<bool> RemoveMenuAsync(MenuId menuId, string rootPath, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            bool result = await _decorated.RemoveMenuAsync(menuId, rootPath, cancellationToken);

            if (result)
            {
                string key = $"{_cacheKey}:{menuId.Value}";
                string? cachedMenu = await _distributedCache.GetStringAsync(key, cancellationToken);

                if (!string.IsNullOrEmpty(cachedMenu))
                {
                    await _distributedCache.RemoveAsync(key, cancellationToken);
                }

                return true;
            }            

            return false;
        }                           
        public async Task<Menu> UpdateMenuAsync(MenuId menuId, Menu updatedMenu, string rootPath,
                                                CancellationToken cancellationToken, List<Guid> imageIds = null!)
        {
            Menu menu = new();
            if(updatedMenu is not null)
            {
                menu = await _decorated.UpdateMenuAsync(menuId, updatedMenu, rootPath, cancellationToken, imageIds);

                if(menu is not null)
                {
                    string key = $"{_cacheKey}:{menuId.Value}";
                    await _distributedCache.SetStringAsync(
                            key,
                            JsonConvert.SerializeObject(menu, new MenuConverter()),
                            cancellationToken);
                }               
            }

            return menu!;
        }
        private async Task<List<Menu>> GetMenuCollectionFromCache(CancellationToken cancellationToken, int categoryId = 0)
        {
            var redis = ConnectionMultiplexer
               .Connect(_configuration.GetConnectionString(ConnectionNames.Redis)!);

            var keys = redis.GetServer(_configuration.GetConnectionString(ConnectionNames.Redis)!)
                            .Keys(pattern: $"{_cacheKey}*");

            var menusList = new List<Menu>();
            if (keys.Count() is not 0)
            {
                foreach (var key in keys)
                {
                    string? value = await _distributedCache.GetStringAsync(key!, cancellationToken);

                    Menu menu = JsonConvert.DeserializeObject<Menu>(
                        value!,
                        new JsonSerializerSettings
                        {
                            ConstructorHandling =
                                ConstructorHandling.AllowNonPublicDefaultConstructor,
                            ContractResolver = new PrivateResolver(),
                            Converters = { new MenuConverter() }
                        })!;

                    menusList.Add(menu);
                }

                if (categoryId is not 0)
                {
                    if (menusList is not null || menusList!.Count() is not 0)
                        menusList = menusList!
                            .Where(u => u.Category.Name == Category.FromId(categoryId).Name).ToList();
                }
            }

            return menusList!;
        }
        private async Task SetMenuCollectionToCache(List<Menu> menus, CancellationToken cancellationToken)
        {
            if (menus.Count() is not 0 || menus is not null)
            {
                foreach (var menu in menus)
                {
                    string key = $"{_cacheKey}:{menu.Id.Value}";

                    await _distributedCache.SetStringAsync(
                        key,
                        JsonConvert.SerializeObject(menu, new MenuConverter()),
                        cancellationToken);
                }
            }
            return;
        }

    }
}
