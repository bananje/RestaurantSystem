namespace LuckyFoodRestaurantSystem.Shared.Features;

public static class Strings
{
    public static string ServiceUnavailable(string serviceName, string? reason)
        => $"Сервис {serviceName} недоступен! Причина: {reason}";
}
