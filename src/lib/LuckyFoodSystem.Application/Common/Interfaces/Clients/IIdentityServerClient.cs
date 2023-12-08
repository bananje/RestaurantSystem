using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.Application.Common.Options;

namespace LuckyFoodSystem.Application.Common.Interfaces.Clients
{
    public interface IIdentityServerClient
    {
        Task<Token> GetApiToken(IdentityServerApiOptions options);
    }
}
