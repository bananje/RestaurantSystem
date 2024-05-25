using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Entity;

public class Address : Entity<AddressId>
{
    public string City { get; private set; } = string.Empty;

    public string Street { get; private set; } = string.Empty;

    public string House { get; private set; } = string.Empty;

    public string ApartmentNum { get; private set; } = string.Empty;

    public override string ToString()
    {
        return $"{City} {Street} {House} {ApartmentNum}";
    }
}
