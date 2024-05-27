using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Entity;
using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Rules;

public class TheCustomerMustHaveAnAddress : IBusinessRule
{
    public TheCustomerMustHaveAnAddress(Address address)
    {
        Address = address;
    }

    public Address Address { get; private set; }

    public string ErrorMessage => "Покупатель должен указать адрес доставки!";

    public bool IsBroken()
    {
        return Address is null ? true : false;
    }
}
