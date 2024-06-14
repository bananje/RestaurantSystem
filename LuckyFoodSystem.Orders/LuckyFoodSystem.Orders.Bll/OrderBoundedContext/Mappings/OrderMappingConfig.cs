using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.ProductEntity;
using Mapster;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Mappings;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderInfo>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CurrentStatus, src => src.CurrentStatus.Name)
            .Map(dest => dest.PaymentStatus, src => src.PaymentStatus.Name)
            .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
            .Map(dest => dest.CourierId, src => src.CourierId.Value)
            .Map(dest => dest.TotalPrice, src => src.TotalPrice)
            .Map(dest => dest.DeliveryAddress, src => src.DeliveryAddress.ToString())
            .Map(dest => dest.IsClosed, src => src.IsClosed)
            .Map(dest => dest.ClosedWithStatus, src => src.ClosedWithStatus.Name)
            .Map(dest => dest.OrderStatusChangedAt, src => src.OrderStatusChangedAt)
            .Map(dest => dest.OrderLines, src => src.OrderLines.Adapt<IReadOnlyCollection<OrderLineInfo>>());

        config.NewConfig<OrderLine, OrderLineInfo>()
            .Map(dest => dest.OrderLineId, src => src.Id.Value)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Discount, src => src.Discount)
            .Map(dest => dest.ReadyStatus, src => src.ReadyStatus.Name)
            .Map(dest => dest.Product, src => src.Product);

        config.NewConfig<Product, ProductInfo>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.ProductImageUrl, src => src.ProductImageUrl)
            .Map(dest => dest.Status, src => src.Status.Name)
            .Map(dest => dest.ShortDescription, src => src.ShortDescription.Value)
            .Map(dest => dest.Price, src => src.Price.Value)
            .Map(dest => dest.Discount, src => src.Discount.Value)
            .Map(dest => dest.BaseDiscount, src => src.BaseDiscount!.Value)
            .Map(dest => dest.MaxDiscount, src => src.MaxDiscount)
            .Map(dest => dest.PriceWithDiscount, src => src.PriceWithDiscount)
            .Map(dest => dest.Weight, src => src.Weight.Value)
            .Map(dest => dest.WeightUnit, src => src.WeightUnit.Name);
    }
}
