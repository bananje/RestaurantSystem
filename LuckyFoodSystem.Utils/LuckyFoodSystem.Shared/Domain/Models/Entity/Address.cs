namespace LuckyFoodSystem.Shared.Domain.Models.Entity;

public class Address : Entity<AddressId>
{
    public Address(
        string city,
        string street,
        string house,
        string apartmentNum)
    {
        City = city;
        Street = street;
        House = house;
        ApartmentNum = apartmentNum;
    }

    public string City { get; private set; } = string.Empty;

    public string Street { get; private set; } = string.Empty;

    public string House { get; private set; } = string.Empty;

    public string ApartmentNum { get; private set; } = string.Empty;

    public override string ToString()
    {
        return $"{City} {Street} {House} {ApartmentNum}";
    }
}
