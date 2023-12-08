using Microsoft.AspNetCore.Http;

namespace RestaurantMenu.Utils.IServices
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }
    }
}
