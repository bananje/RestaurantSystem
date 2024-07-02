namespace LuckyFoodSystem.Shared.Contracts;

public record GetCustomerDataResponse
{
    public string Email { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string MiddleName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string Street { get; init; } = string.Empty;

    public string House { get; init; } = string.Empty;

    public string ApartmentNum { get; init; } = string.Empty;
}
