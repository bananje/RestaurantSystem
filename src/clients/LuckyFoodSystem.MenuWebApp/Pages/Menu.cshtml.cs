using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantMenu.App.Pages
{
    public class MenuModel : PageModel
    {
        public bool IsSelectedMenu = false;
        public string Header = "Lucky" + WC.Categories.Food.ToString();

        public MenuModel()
        {

        }

        public IEnumerable<Product?>? Products { get; set; }
        public List<Menu> Menu { get; set; }

        [BindProperty]
        public int ProductCategory { get; set; } = 1;

        [BindProperty]
        public int MenuId { get; set; }
        public async Task OnGetAsync() 
        {
            var t = User.Claims;
            var userClaims = HttpContext.User.Claims;
            HttpContext.Session.SetInt32(WC.SessionValues.ProductCategory.ToString(), ProductCategory);
            Header = ProductCategory == 1 ? WC.Categories.Food.ToString() : WC.Categories.Bar.ToString();
            Products = await _productService.GetProductsAsync(ProductCategory);
            Menu = await _menuService.GetMenuAsync(ProductCategory);
        }
        public async Task OnPostGetProducts()
        {
            HttpContext.Session.SetInt32(WC.SessionValues.ProductCategory.ToString(), ProductCategory);
            IsSelectedMenu = false;
            Menu = await _menuService.GetMenuAsync(ProductCategory);
            Products = await _productService.GetProductsAsync(ProductCategory);
            Header = ProductCategory == 1 ? WC.Categories.Food.ToString() : WC.Categories.Bar.ToString();
            HttpContext.Session.Remove(WC.SessionValues.isActionSucces.ToString());
        }        
        public async Task OnPostGetProductsByMenu()
        {
            int? categoryId = HttpContext.Session.GetInt32(WC.SessionValues.ProductCategory.ToString());
            IsSelectedMenu = true;
            Menu = await _menuService.GetMenuAsync(categoryId);
            Products = await _productService.GetProductsByMenuAsync(MenuId, categoryId);
            HttpContext.Session.Remove(WC.SessionValues.isActionSucces.ToString());

            //get menu name on cache
            string? menuTitle = Menu.FirstOrDefault(u => u.MenuId == MenuId).Name;
            Header = menuTitle;
        }      

        public IActionResult CloseNotice()
        {
            HttpContext.Session.Remove(WC.SessionValues.isActionSucces.ToString());
            return Redirect("/Menu");
        }
    }
}
