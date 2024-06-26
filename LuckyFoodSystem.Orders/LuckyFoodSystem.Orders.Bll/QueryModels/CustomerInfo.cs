using LuckyFoodSystem.Orders.Bll.Features.Mediatr;

namespace LuckyFoodSystem.Orders.Bll.QueryModels;

public record CustomerInfo : IQueryObject
{
    public Guid CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public int OrdersCount { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public string FullName => $"{MiddleName} {FirstName} {LastName}";

    public ICollection<OrderInfo> Orders { get; set; } = null!;

    public long Version { get; set; }
}