using LuckyFoodSystem.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Infrastructure.Services
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
