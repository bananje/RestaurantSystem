namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Common.Options;

public class MartenConfig
{
    public static string SectionName = nameof(MartenConfig);

    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseSchemaName { get; set; } = string.Empty;
}
