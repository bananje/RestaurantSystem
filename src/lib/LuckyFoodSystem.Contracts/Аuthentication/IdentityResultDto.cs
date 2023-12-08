using Microsoft.AspNetCore.Identity;

namespace LuckyFoodSystem.Contracts.Authentication
{
    public record IdentityResultDto(bool IsSucceeded, 
                                    IEnumerable<IdentityError>? Errors);
}
