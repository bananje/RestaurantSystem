namespace LuckyFoodSystem.Contracts.User
{
    public record UserPasswordChangeRequestDto(string UserName,
                                            string CurrentPassword,
                                            string NewPassword);
}
