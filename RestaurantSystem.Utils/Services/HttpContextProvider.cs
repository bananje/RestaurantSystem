using Microsoft.AspNetCore.Http;
using RestaurantMenu.Utils.IServices;

namespace RestaurantMenu.Utils.Services
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public HttpContext CurrentHttpContext => _httpContextAccessor.HttpContext;
    }
}
