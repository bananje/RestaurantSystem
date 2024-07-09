namespace LuckyFoodSystem.Shared.Contracts.OrderService.Contracts;

public record GetDeliveryAddress
{
    public Guid CustomerId { get; init; }
}

public record GetDeliveryAddressResponse
{
    public string City { get; init; } = string.Empty;

    public string Street { get; init; } = string.Empty;

    public string House { get; init; } = string.Empty;

    public string ApartmentNum { get; init; } = string.Empty;
}
