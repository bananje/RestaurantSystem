using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.Models.Models;
using RestaurantMenu.Models.VM;
using RestaurantMenu.Utils;
using RestaurantMenu.Utils.IServices;

namespace RestaurantSystem.MenuApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment,
                                 IMapper mapper, IMenuService menuService)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _menuService = menuService;
        }
        public async Task<IActionResult> Index(int id)
        {
            int? categoryId = HttpContext.Session.GetInt32(WC.SessionValues.ProductCategory.ToString());
            ProductVM productVM = new();

            // loaded product model for editing
            if (id != 0)
            {
                // loaded product for rendering on page for editing
                var cachedProducts = await _productService.GetProductsAsync(categoryId);
                Product? selectedProduct = cachedProducts.FirstOrDefault(u => u.ProductId == id);
                productVM.Product = _mapper.Map<ProductDTO>(selectedProduct);
            }
            // sending an empty product for adding
            else
            {
                productVM.Product = new ProductDTO();
            }

            // loaded and sorted menus for rendering on selector 
            var allMenus = await _menuService.GetMenuAsync(null);
            productVM.MenusList = allMenus.Where(u => u.CategoryId == categoryId).ToList();

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertProductAsync(ProductDTO? product)
        {
            int categoryId = (int)HttpContext.Session.GetInt32(WC.SessionValues.ProductCategory.ToString());
            product.CategoryId = categoryId;
            string webRootPath = _webHostEnvironment.WebRootPath;

            bool result = await _productService.UpsertProductAsync(product, webRootPath);
            HttpContext.Session.SetString(WC.SessionValues.isActionSucces.ToString(), result.ToString());
            
            return Redirect("/Menu");
        }
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if(id != 0)
            {
                string rootPath = _webHostEnvironment.WebRootPath;
                int? categoryId = HttpContext.Session.GetInt32(WC.SessionValues.ProductCategory.ToString());
                bool result = await _productService.DeleteProductAsync(id, categoryId, rootPath);
                HttpContext.Session.SetString(WC.SessionValues.isActionSucces.ToString(), result.ToString());
            }
            return Redirect("/Menu");
        }
    }
}
