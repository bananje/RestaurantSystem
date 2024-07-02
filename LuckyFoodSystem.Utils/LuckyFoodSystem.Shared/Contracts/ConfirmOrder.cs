using LuckyFoodSystem.Shared.Models;

namespace LuckyFoodSystem.Shared.Contracts;

public record ConfirmOrder
{
    public Guid CustomerId { get; init; }

    public ICollection<OrderLineInfo> OrderLines { get; init; } = [];

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string House { get; set; } = string.Empty;

    public string ApartmentNum { get; set; } = string.Empty;
}