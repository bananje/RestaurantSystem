using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CourierAggregate;

public class Courier : AggregateRoot<CourierId>
{
    public string FirstName { get; private set; } = null!;

    public string MiddleName { get; private set; } = null!;

    public string LastName { get; private set; } = string.Empty;

    public CourierEmail Email { get; private set; } = null!;

    public CourierPhone Phone { get; private set; } = null!;

    public CourierStatus Status { get; private set; } = null!;

    protected override void Apply(DomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
