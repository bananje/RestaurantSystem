using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IImageService _imageService;
        private readonly LuckyFoodDbContext _context;
        public MenuRepository(IHttpContextProvider httpContextProvider,
                              IImageService imageService,
                              LuckyFoodDbContext context)
        {
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
                }
                finally
                {
                    await Task.CompletedTask;
                }
            }
        }

        public async Task<Menu>? GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            Menu? menu = _context.Menus.Include(u => u.Images)
                                       .AsEnumerable()
                                       .SingleOrDefault(u => u.Id.Value == menuId.Value);

            return menu;
        }

        public async Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken = default)
            => await _context.Menus.Include(u => u.Images).ToListAsync();

        public async Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
            => await _context.Menus
                        .Where(u => u.Category == Category.FromId(categoryId)).ToListAsync();
    }
}
