using RestaurantMenu.Models.Models;
using RestaurantSystem.Models.Models;

namespace RestaurantMenu.Models.VM
{
    public class ProductVM
    {
        public ProductDTO? Product { get; set; }
        public List<Menu>? MenusList { get; set; }
    }
}
