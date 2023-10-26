namespace RestaurantMenu.Utils
{
    public class WC
    {       
        public const string ProductImagePath = @"\images\product\";
        public const string MenuCacheKey = "MenuKey";
        public const string CategoryCacheKey = "CategoryKey";
        public enum Categories
        {
            Food,
            Bar
        }
        public enum Entities
        {
            Menu,
            Product
        }
        public enum SessionValues
        {
            ProductCategory,
            isActionSucces
        }
        public enum ClientsName
        {
            MenuWebApp,
        }
    }
}
