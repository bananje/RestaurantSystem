using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.Application.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using LuckyFoodSystem.Contracts.Product;
using MapsterMapper;
using LuckyFoodSystem.Contracts.Menu;

namespace RestaurantMenu.App.Pages
{
    /// <summary>
    /// Обработка страницы интерактивного веб-меню
    /// </summary>
    public class MenuModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public MenuModel(IHttpClientFactory httpClientFactory,
                         IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        
        // Для 
        public bool IsSelectedMenu = false;
        public string Header = $"{Titles.LuckyFood} {Titles.Food}";

        // текущий список отображаемых продуктов
        public ProductResponse Products { get; set; }

        // текущий список отображаемых меню
        public MenuResponse Menus { get; set; }

        /// Бинд значения (ID) текущей категории      
        [BindProperty]
        public int Category { get; set; } = 1;

        /// Бинд значения (ID) текущего меню
        [BindProperty]
        public int MenuId { get; set; }

        /// <summary>
        /// Инициализация страницы и первичная загрузка
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync() 
        {
            var client = GetConfiguredClient();

            // обращение к точкам API
            HttpResponseMessage productEndpoint = await client.GetAsync("products");
            HttpResponseMessage menuEndpoint = await client.GetAsync("menus");

            // получение полного списка продуктов
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // получение полного списка меню
            var menus = await menuEndpoint.Content.ReadAsStringAsync();
            Menus = JsonConvert.DeserializeObject<MenuResponse>(menus)!;  
            
            client.Dispose();
        }

        /// <summary>
        /// Получение продуктов по выбранной категории
        /// </summary>
        /// <returns></returns>
        public async Task OnPostGetProductsByCategory()
        {
            var client = GetConfiguredClient();

            HttpResponseMessage productEndpoint = await client.GetAsync($"products/{Category}");
            HttpResponseMessage menuEndpoint = await client.GetAsync($"menu/{Category}");

            // получение списка продуктов по категории
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // получение списка меню по категории
            var menus = await menuEndpoint.Content.ReadAsStringAsync();
            Menus = JsonConvert.DeserializeObject<MenuResponse>(menus)!;

            client.Dispose();
        }

        /// <summary>
        /// Получение меню по выбранному меню
        /// </summary>
        /// <returns></returns>
        public async Task OnPostGetProductsByMenu()
        {
            var client = GetConfiguredClient();

            HttpResponseMessage productEndpoint = await client.GetAsync($"products/{MenuId}");
            HttpResponseMessage menuEndpoint = await client.GetAsync($"menu/{Category}");

            // получение списка продуктов по категории
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // получение списка меню по категории
            var menus = await menuEndpoint.Content.ReadAsStringAsync();
            Menus = JsonConvert.DeserializeObject<MenuResponse>(menus)!;

            client.Dispose();
        }

        //public IActionResult CloseNotice()
        //{
        //    HttpContext.Session.Remove(WC.SessionValues.isActionSucces.ToString());
        //    return Redirect("/Menu");
        //}

        private HttpClient GetConfiguredClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7055");

            return client;
        }
    }
}
