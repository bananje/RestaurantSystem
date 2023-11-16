using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Infrastructure.Services.MemoryCacheService;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository
{
    public class CachedMenuRepository : IMenuRepository
    {
        private readonly IMenuRepository _decorated;
        private readonly IDistributedCache _distributedCache;

        public CachedMenuRepository(IMenuRepository decorated,
                                    IDistributedCache distributedCache)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
        }

        public Task AddMenuAsync(Menu menu, string rootPath)
        {
            throw new NotImplementedException();
        }

        public async Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default)
        {
            string key = $"menus{CacheSettings.ObjKey}:{menuId.Value}";
            Menu? menu;

            string? cachedMenu = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedMenu))
            {
                menu = await _decorated.GetMenuByIdAsync(menuId, cancellationToken);

                if (menu is null) return menu;

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(menu, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore}),
                    cancellationToken);

                return menu;
            }

            menu = JsonConvert.DeserializeObject<Menu>(
                cachedMenu,
                new JsonSerializerSettings
                {
                    ConstructorHandling =
                        ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            return menu;
        }
        
        public async Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken = default)
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");            
            var keys = redis.GetServer("localhost:6379").Keys(pattern: $"menus{CacheSettings.ObjKey}*");

            var menusList = new List<Menu>();

            if(keys.Count() is not 0)
            {
                foreach (var key in keys)
                {
                    string value = await _distributedCache.GetStringAsync(key, cancellationToken);

                    Menu menu = JsonConvert.DeserializeObject<Menu>(
                        value,
                        new JsonSerializerSettings
                        {
                            ConstructorHandling =
                                ConstructorHandling.AllowNonPublicDefaultConstructor,
                            ContractResolver = new PrivateResolver()
                        })!;

                    menusList.Add(menu);
                }
                return menusList;
            }

            menusList = await _decorated.GetMenusAsync(cancellationToken);

            if(menusList.Count() is not 0 || menusList is not null)
            {
                foreach (var menu in menusList)
                {
                    string key = $"menus{CacheSettings.ObjKey}:{menu.Id.Value}";

                    await _distributedCache.SetStringAsync(
                        key,
                        JsonConvert.SerializeObject(menu, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        cancellationToken);
                }
            }

            return menusList;
        }

        public Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
