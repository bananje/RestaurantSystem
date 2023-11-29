namespace LuckyFoodSystem.Contracts.User
{
    public record UserPasswordChangeRequest(string UserName,
                                            string CurrentPassword,
                                            string NewPassword);
}
