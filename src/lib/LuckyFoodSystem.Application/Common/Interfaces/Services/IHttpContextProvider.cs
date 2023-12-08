using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Application.Common.Interfaces.Services
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }
    }
}
