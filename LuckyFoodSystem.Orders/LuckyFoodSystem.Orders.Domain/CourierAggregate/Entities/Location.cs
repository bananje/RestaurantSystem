using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CourierAggregate.Entities;

public class Location : Entity<LocationId>
{
    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public DateTime LastUpdated { get; private set; }

    public Location(double latitude, double longitude, DateTime lastUpdated)
    {
        if (latitude < -90 || latitude > 90) 
            throw new BusinessException("Latitude must be between -90 and 90 degrees.");

        if (longitude < -180 || longitude > 180) 
            throw new BusinessException("Longitude must be between -180 and 180 degrees.");

        Latitude = latitude;
        Longitude = longitude;
        LastUpdated = lastUpdated;
    }
}
