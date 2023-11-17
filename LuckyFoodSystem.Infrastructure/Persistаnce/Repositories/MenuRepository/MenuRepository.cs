using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ImageAggregate.ValueObjects;
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
        public async Task<Menu> GetMenuByIdAsync(MenuId menuId, CancellationToken cancellationToken = default)            
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
                        .Where(u => u.Category == Category.FromId(categoryId)).Include(u => u.Images).ToListAsync();
        public async Task AddMenuAsync(Menu menu, string rootPath, CancellationToken cancellationToken = default)
        {
            if (menu is not null)
            {
                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;

                if (files.Count() is not 0 || files is not null)
                {
                    List<Image> images = await _imageService.LoadImages(files, rootPath);
                    images.ForEach(async img => { await _context.Images.AddAsync(img); });
                    menu.AddImages(images);
                }

                try
                {                   
                    _context.Menus.Add(menu);
                    _context.SaveChanges();
                }
                finally
                {
                    await Task.CompletedTask;
                }
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
            if(images is not null || images.Count() is not 0)
            {
                _imageService.RemoveImage(rootPath, images);
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

        public async Task UpdateMenuAsync(Menu updatedMenu, List<Guid> imageIds, 
                                          string rootPath, CancellationToken cancellationToken = default)
        {           
            // upload new files 
            var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;
            if (files.Count() is not 0)
            {
                List<Image> images = await _imageService.LoadImages(files, rootPath);
                images.ForEach(async img => { await _context.Images.AddAsync(img); });
                updatedMenu.AddImages(images);
            }

            if (imageIds.Count() is not 0)
            {              
                List<Image> fdf = new();
                foreach (var item in imageIds)
                {
                    Image? img =  _context.Images.AsEnumerable()
                        .FirstOrDefault(u => u.Id.Value == item);

                    _context.Images.Remove(img);
                    fdf.Add(img);
                }
                

                await _context.SaveChangesAsync();

                fdf.RemoveAll(item => imageIds.Contains(item.Id.Value));
                updatedMenu.AddImages(fdf);
            }

            _context.Menus.Update(updatedMenu);
            await _context.SaveChangesAsync();         
        }
    }
}
