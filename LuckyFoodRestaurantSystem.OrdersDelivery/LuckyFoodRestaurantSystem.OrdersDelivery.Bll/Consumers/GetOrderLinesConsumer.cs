using LuckyFoodRestaurantSystem.Contracts.Shared;
using LuckyFoodRestaurantSystem.ProductStock.Contracts;
using MassTransit;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Consumers;

public class GetOrderLinesConsumer : IConsumer<GetOrderLinesRequest>
{
    public async Task Consume(ConsumeContext<GetOrderLinesRequest> context)
    {
        var test = new List<OrderLineResponseStruct>();

        var testProduct = new ProductStruct()
        {
            BaseDiscount = 0,
            Description = "f",
            Discount = 0,
            MaxDiscount = 0,
            Price = 0,
            PriceWithDiscount = 0,
            ProductId = new Guid(),
            ProductImageUrl = "fdf",
            ShortDescription = "fdf",
            Status = "df",
            Title = "fdfff",
            Weight = 0,
            WeightUnit = "Fd"
        };

   
        var testOrderLine = new OrderLineResponseStruct()
        {
           Product = testProduct,
           Quantity = 1,
        };

        test.Add(testOrderLine);

        await context.RespondAsync<GetOrderLinesResponse>(new
        {
            OrderLines = test,
            IsConfirmed = true
        });
    }
}
