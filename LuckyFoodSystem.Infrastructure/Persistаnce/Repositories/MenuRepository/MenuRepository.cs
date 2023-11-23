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
        public async Task<Menu?> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default)      
            => await _context.Menus.Include(u => u.Images)
                                   .AsNoTracking().FirstOrDefaultAsync(u => u.Id == menuId);       
        public async Task<List<Menu>> GetMenusAsync(CancellationToken cancellationToken = default)
            => await _context.Menus.Include(u => u.Images).ToListAsync(cancellationToken);
        public async Task<List<Menu>> GetMenusByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
            => await _context.Menus
                     .Where(u => u.Category == Category.FromId(categoryId)).Include(u => u.Images)
                     .ToListAsync(cancellationToken);
        public async Task AddMenuAsync(Menu menu, string rootPath, CancellationToken cancellationToken = default)
        {
            if (menu is not null)
            {
                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;

                if (files.Count() is not 0 || files is not null)
                {
                    List<Image> images = await _imageService.LoadImages(files, rootPath);
                    menu.AddImages(images);
                }
                             
                await _context.Menus.AddAsync(menu);
                await _context.SaveChangesAsync(cancellationToken);               
            }
        }
        public async Task<bool> RemoveMenuAsync(MenuId menuId, string rootPath, CancellationToken cancellationToken = default)
        {
            var selectedMenu = _context.Menus
                    .Include(m => m.Images)
                    .AsEnumerable()
                    .SingleOrDefault(m => m.Id.Value == menuId.Value);

            if (selectedMenu is null) return false;

            List<Image> images = selectedMenu.Images.ToList();
            if(images is not null || images!.Count() is not 0)
            {
                _imageService.RemoveFromPath(rootPath, images);
                _context.Images.RemoveRange(images);
            }

            try
            {
                _context.Menus.Remove(selectedMenu);
                await _context.SaveChangesAsync();

                return true;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }                   
        public async Task<Menu> UpdateMenuAsync(MenuId menuId, Menu updatedMenu, string rootPath, CancellationToken cancellationToken = default, List<Guid> imageIds = null!)
        {
            var thisMenu = _context.Menus.AsEnumerable().FirstOrDefault(u => u.Id.Value == menuId.Value);
            if (thisMenu is not null)
            {
                thisMenu = Menu.Update(thisMenu, updatedMenu);
                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;

                if (files.Count() is not 0)
                {
                    List<Image> images = await _imageService.LoadImages(files, rootPath);
                    thisMenu.AddImages(images);
                }

                if (imageIds is not null)
                {
                    List<Guid> imagesForDeleting = _imageService.RemoveImages(rootPath, imageIds);
                    thisMenu.RemoveImages(imagesForDeleting);
                }

                _context.Menus.Update(thisMenu);
                await _context.SaveChangesAsync();
            }

            return thisMenu!;
        }
    }
}
