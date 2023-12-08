using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.Common.Entities
{
    public class Adress : Entity<AdressId>
    {
        private Adress(AdressId adressId,
                       string city,
                       string street,
                       string house,
                       string apartmentNum,
                       string userId)
        {
            City = city;
            Street = street;
            House = house;
            ApartmentNum = apartmentNum;
            UserId = userId;
        }
        public Adress() { }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string House { get; private set; }
        public string ApartmentNum { get; private set; }
        public string? UserId { get; private set; }

        public static Adress Create(string city,
                                    string street,
                                    string house,
                                    string apartmentNum,
                                    string userId)
             => new(AdressId.CreateUnique(), city, street, house, apartmentNum, userId);
        public static Adress Update(Adress oldAdress, Adress newAdress)
        {
            var updatedProperties = newAdress.GetType().GetProperties();
            foreach (var property in updatedProperties)
            {
                var newValue = property.GetValue(newAdress);
              
                if (newValue is not null)
                {
                    var oldProperty = oldAdress.GetType().GetProperty(property.Name);
                    if (oldProperty is not null && oldProperty.CanWrite)
                    {
                        oldProperty.SetValue(oldAdress, newValue);
                    }
                }
            }
            return oldAdress;
        }
    }
}