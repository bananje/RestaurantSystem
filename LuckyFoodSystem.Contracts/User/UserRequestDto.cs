namespace LuckyFoodSystem.Contracts.User
{
    public record UserRequestDto(string UserName,
                                 string Email,
                                 bool EmailConfirmed,
                                 string Password,
                                 string PhoneNumber,
                                 Dictionary<string, string> Claims,
                                 string id = null!);
}
