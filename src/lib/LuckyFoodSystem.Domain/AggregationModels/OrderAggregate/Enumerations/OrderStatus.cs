using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.Enumerations
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Issued = new(1, nameof(Issued));
        public static OrderStatus Delivered = new(2, nameof(Delivered));
        public static OrderStatus Received = new(3, nameof(Received));
        public OrderStatus(int id, string name) : base(id, name)
        {
        }
        public static OrderStatus FromId(int id)
           => GetAll<OrderStatus>().FirstOrDefault(x => x.Id == id)!;
        public static OrderStatus FromName(string name)
            => GetAll<OrderStatus>().FirstOrDefault(x => x.Name == name)!;
    }
}