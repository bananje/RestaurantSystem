using LuckyFoodSystem.Shared.Mediatr;

namespace LuckyFoodSystem.Shared.Models;

public class CourierInfo : IQueryObject
{
    public Guid CourierId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public Guid CurrentDeliveringOrder { get; set; }

    public IReadOnlyCollection<OrderInfo> CompleteOrders { get; set; } = null!;

    public string FullName => $"{MiddleName} {FirstName} {LastName}";

    public long Version { get; set; }
}
