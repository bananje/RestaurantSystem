namespace LuckyFoodSystem.Orders.Bll.Features.Mediatr;

public interface IQueryObject
{
    public Guid Id { get; set; }

    public long Version { get; set;  }
}
