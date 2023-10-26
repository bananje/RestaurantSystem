using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestaurantMenu.Models.Models;
using RestaurantMenu.Utils.IServices;
using RestaurantSystem.Models;

namespace RestaurantMenu.Utils.Services
{
    public class ProductService : IProductService
    {
        private readonly RestaurantSystemDbContext _db;
        private readonly IMapper _mapper;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly IValidatorService _validatorService;
        public ProductService(RestaurantSystemDbContext db, IHttpContextProvider httpContextProvider,
                              IMapper mapper, IMemoryCache memoryCache, IValidatorService validatorService)
        {    
            _mapper = mapper;
            _httpContextProvider = httpContextProvider;
            _memoryCache = memoryCache;
            _validatorService = validatorService;
            _db = db;
        }
        public async Task<List<Product>?> GetProductsAsync(int? categoryId)
        {
            _memoryCache.TryGetValue(WC.CategoryCacheKey + categoryId, out List<Product>? cachedProducts);
            if (cachedProducts == null)
            {
                cachedProducts = await _db.Products.Include(u => u.Menu)
                                                   .Where(u => u.Category.CategoryId == categoryId)
                                                   .OrderByDescending(u => u.ImageId).ToListAsync();
                if (cachedProducts is not null)
                {
                    _memoryCache.Set(WC.CategoryCacheKey + categoryId, cachedProducts, TimeSpan.FromHours(1));
                }
            }
            return cachedProducts;
        }
        public async Task<IEnumerable<Product>?> GetProductsByMenuAsync(int? menuId, int? key)
        {
            var cachedProducts = await GetProductsAsync(key);
            if (cachedProducts != null)
            {
                var sortedProducts = cachedProducts.Where(u => u.MenuId == menuId);
                return sortedProducts;
            }
            return null;
        }
        public async Task<bool> UpsertProductAsync(ProductDTO? product, string webRootPath)
        {
            if (product != null)
            {
                bool isValid = _validatorService.ValidateObject(product);
                if (!isValid) return false;

                if (isValid)
                {
                    _memoryCache.TryGetValue(WC.CategoryCacheKey + product.CategoryId, out List<Product?>? cachedProducts);
                    string upload = webRootPath + WC.ProductImagePath; // path to wwwroot
                    Product prodToCache = new();
                    var files = _httpContextProvider.CurrentHttpContext.Request.Form.Files;

                    // edit/update product on db
                    if (product.ProductId != 0)
                    {
                        string fileId = null;

                        if (files.Count != 0)
                        {
                            Image img = await _db.Images.FirstOrDefaultAsync(u => u.ImageId == product.ImageId);

                            if (img is not null)
                            {
                                // deleting old img on folder
                                var oldFile = Path.Combine(upload, img.Path);
                                if (File.Exists(oldFile))
                                {
                                    File.Delete(oldFile);
                                }
                            }

                            // adding a new file
                            string extension = Path.GetExtension(files[0].FileName);
                            Guid fileName = Guid.NewGuid();
                            using (FileStream fileStream = new(Path.Combine(upload, fileName + extension), FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }
                            await _db.Images.AddAsync(new Image() { ImageId = fileName, Path = fileName + extension });
                            fileId = fileName.ToString();
                        }

                        Product changeProduct = cachedProducts.FirstOrDefault(u => u.ProductId == product.ProductId);
                        prodToCache = _mapper.Map<Product>(product);
                        if (fileId is not null)
                        {
                            prodToCache.ImageId = Guid.Parse(fileId);
                        }

                        cachedProducts.Remove(changeProduct);
                        _db.Products.Update(prodToCache);
                    }
                    // add a new product to db
                    else
                    {
                        Product newProduct = _mapper.Map<Product>(product);

                        if (files != null)
                        {
                            string extension = Path.GetExtension(files[0].FileName);
                            Guid fileName = Guid.NewGuid();

                            using (FileStream fileStream = new(Path.Combine(upload, fileName.ToString() + extension), FileMode.Create))
                            {
                                files[0].CopyToAsync(fileStream);
                            }
                            await _db.Images.AddAsync(new Image() { Path = fileName + extension, ImageId = fileName });
                            newProduct.ImageId = fileName;
                        }

                        await _db.Products.AddAsync(newProduct);
                        prodToCache = newProduct;
                    }

                    try
                    {
                        await _db.SaveChangesAsync();
                        prodToCache.Menu = await _db.Menus.FindAsync(product.MenuId);
                        cachedProducts.Add(prodToCache);
                        _memoryCache.Set(WC.CategoryCacheKey + product.CategoryId, cachedProducts.OrderBy(u => u.ProductId), TimeSpan.FromHours(1));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public async Task<bool> DeleteProductAsync(int? productId, int? cacheKey, string? webRootPath)
        {
            if (productId != null)
            {
                _memoryCache.TryGetValue(WC.CategoryCacheKey + cacheKey, out List<Product>? cachedProducts);
                if(cachedProducts == null)
                {
                    cachedProducts = await GetProductsAsync(productId);
                }
                var product = cachedProducts.FirstOrDefault(u => u.ProductId == productId);

                if (product.ImageId != null)
                {
                    string upload = webRootPath + WC.ProductImagePath; // path to wwwroot
                    Image? img = await _db.Images.FindAsync(product.ImageId);
                    _db.Remove(img);

                    // deleting old img on folder
                    var oldFile = Path.Combine(upload, img.Path);
                    if (File.Exists(oldFile))
                    {
                        File.Delete(oldFile);
                    }
                }

                try
                {
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                    cachedProducts.Remove(product);
                    _memoryCache.Set(WC.CategoryCacheKey + cacheKey, cachedProducts, TimeSpan.FromHours(1));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
