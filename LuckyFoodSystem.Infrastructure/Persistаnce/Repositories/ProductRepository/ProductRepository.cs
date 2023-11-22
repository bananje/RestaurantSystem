using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IMenuRepository _menuRepository;
        private readonly IImageService _imageService;
        private readonly LuckyFoodDbContext _context;
        public ProductRepository(IHttpContextProvider httpContextProvider,
                                 IImageService imageService,
                                 LuckyFoodDbContext context,
                                 IMenuRepository menuRepository)
        {
            _httpContextProvider = httpContextProvider;
            _imageService = imageService;
            _context = context;
            _menuRepository = menuRepository;
        }
        
        public async Task<Product> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
                => await _context.Products.Include(u => u.Images)
                                          .Include(u => u.Menus)
                                          .AsNoTracking().FirstAsync(u => u.Id == productId);       
        public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
                => await _context.Products.Include(u => u.Images)
                                          .Include(u => u.Menus).ToListAsync(cancellationToken);
        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
                => await _context.Products.Where(u => u.Category == Category.FromId(categoryId))
                                          .Include(u => u.Images)
                                          .Include(u => u.Menus)
                                          .ToListAsync(cancellationToken);
        public async Task<List<Product>> GetProductsByMenuAsync(MenuId menuId, CancellationToken cancellationToken = default)
                => await _context.Products.Where(u => u.Menus.Any(i => i.Id == menuId))
                                          .Include(u => u.Menus)
                                          .Include(u => u.Images).ToListAsync(cancellationToken);     
        public async Task AddProductAsync(Product product, List<Guid> menusIds, string rootPath, CancellationToken cancellationToken = default)
        {
            if(product is not null)
            {               
                if(menusIds.Count() is not 0)
                {
                    List<Menu> menus = new();
                    foreach (var menuId in menusIds)
                    {
                        Menu? menu = await _menuRepository.GetMenuByIdAsync(MenuId.Create(menuId));
                        if (menu is null) continue;

                        _context.Attach(menu);
                        menus.Add(menu);
                    }

                    if(menus.Count() is not 0) product.AddMenus(menus);                   
                }

                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;
                if (files.Count() is not 0 && files is not null)
                {
                    List<Image> images = await _imageService.LoadImages(files, rootPath);
                    product.AddImages(images);
                }

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync(cancellationToken);                
            }
        }
        public async Task<bool> RemoveProductAsync(ProductId productId, string rootPath, CancellationToken cancellationToken = default)
        {
            var selectedProduct= _context.Products
                    .Include(m => m.Images)
                    .AsEnumerable()
                    .SingleOrDefault(m => m.Id.Value == productId.Value);

            if (selectedProduct is null) return false;

            List<Image> images = selectedProduct.Images.ToList();
            if (images is not null || images!.Count() is not 0)
            {
                _imageService.RemoveFromPath(rootPath, images!);
                _context.Images.RemoveRange(images!);
            }

            try
            {
                _context.Products.Remove(selectedProduct);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
        public async Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct, string rootPath, 
                                                      CancellationToken cancellationToken = default,
                                                      List<Guid> imageIds = null!, List<Guid> menuAddingIds = null!, List<Guid> menuDeletingIds = null!)
        {
            var thisProduct = await _context.Products.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Id == productId);
            if (thisProduct is not null)
            {
                thisProduct = Product.Update(thisProduct, updatedProduct);
                
                if(menuAddingIds is not null)
                {
                    List<Guid> deletingMenus = new();
                    foreach (var menuId in menuAddingIds)
                    {
                        Menu? menu = await _context.Menus.FirstAsync(u => u.Id == MenuId.Create(menuId));
                        if (menu is null) continue;

                        deletingMenus.Add(menuId);
                    }

                    if(deletingMenus.Count() is not 0) thisProduct.RemoveMenus(deletingMenus);                   
                }

                if(menuDeletingIds is not null)
                {

                }

                if (imageIds is not null)
                {
                    List<Guid> imagesForDeleting = _imageService.RemoveImages(rootPath, imageIds);
                    thisProduct.RemoveImages(imagesForDeleting);
                }

                var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;
                if (files.Count() is not 0)
                {
                    List<Image> images = await _imageService.LoadImages(files, rootPath);
                    thisProduct.AddImages(images);
                }

                _context.Products.Update(thisProduct);
                await _context.SaveChangesAsync();
            }

            return thisProduct!;
        }
    }
}
