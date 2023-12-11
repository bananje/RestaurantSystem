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
    /// ��������� �������� �������������� ���-����
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
        
        // ��� 
        public bool IsSelectedMenu = false;
        public string Header = $"{Titles.LuckyFood} {Titles.Food}";

        // ������� ������ ������������ ���������
        public ProductResponse Products { get; set; }

        // ������� ������ ������������ ����
        public MenuResponse Menus { get; set; }

        /// ���� �������� (ID) ������� ���������      
        [BindProperty]
        public int Category { get; set; } = 1;

        /// ���� �������� (ID) �������� ����
        [BindProperty]
        public int MenuId { get; set; }

        /// <summary>
        /// ������������� �������� � ��������� ��������
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync() 
        {
            var client = GetConfiguredClient();

            // ��������� � ������ API
            HttpResponseMessage productEndpoint = await client.GetAsync("products");
            HttpResponseMessage menuEndpoint = await client.GetAsync("menus");

            // ��������� ������� ������ ���������
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // ��������� ������� ������ ����
            var menus = await menuEndpoint.Content.ReadAsStringAsync();
            Menus = JsonConvert.DeserializeObject<MenuResponse>(menus)!;  
            
            client.Dispose();
        }

        /// <summary>
        /// ��������� ��������� �� ��������� ���������
        /// </summary>
        /// <returns></returns>
        public async Task OnPostGetProductsByCategory()
        {
            var client = GetConfiguredClient();

            HttpResponseMessage productEndpoint = await client.GetAsync($"products/{Category}");
            HttpResponseMessage menuEndpoint = await client.GetAsync($"menu/{Category}");

            // ��������� ������ ��������� �� ���������
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // ��������� ������ ���� �� ���������
            var menus = await menuEndpoint.Content.ReadAsStringAsync();
            Menus = JsonConvert.DeserializeObject<MenuResponse>(menus)!;

            client.Dispose();
        }

        /// <summary>
        /// ��������� ���� �� ���������� ����
        /// </summary>
        /// <returns></returns>
        public async Task OnPostGetProductsByMenu()
        {
            var client = GetConfiguredClient();

            HttpResponseMessage productEndpoint = await client.GetAsync($"products/{MenuId}");
            HttpResponseMessage menuEndpoint = await client.GetAsync($"menu/{Category}");

            // ��������� ������ ��������� �� ���������
            var products = await productEndpoint.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<ProductResponse>(products)!;

            // ��������� ������ ���� �� ���������
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
