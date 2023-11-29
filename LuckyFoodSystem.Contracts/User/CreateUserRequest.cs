namespace LuckyFoodSystem.Contracts.User
{
    public record CreateUserRequest(string UserName,
                                    string Email,
                                    bool EmailConfirmed,
                                    string Password);
}
