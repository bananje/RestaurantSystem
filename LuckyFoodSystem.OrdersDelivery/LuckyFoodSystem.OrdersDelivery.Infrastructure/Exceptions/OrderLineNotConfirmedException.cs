using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;
using System.Text;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Exceptions;

public class OrderLineNotConfirmedException : Exception
{
    public OrderLineNotConfirmedException(Guid orderId, IList<(OrderLine OrderLine, string Reason)> orderLines) : base(CreateMessage(orderId, orderLines)) 
    {

    }

    private static string CreateMessage(Guid orderId, IList<(OrderLine OrderLine, string Reason)> orderLines)
    {
        StringBuilder messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"ID Заказа: {orderId}");
        messageBuilder.AppendLine("Отказ в подтверждении позиций заказа:");

        foreach (var line in orderLines)
        {
            messageBuilder.AppendLine($"Товар ID: {line.OrderLine.Product.ProductId}, Количество: {line.OrderLine.Quantity}");
            messageBuilder.AppendLine($"Причина отказа: {line.Reason}");
        }

        return messageBuilder.ToString();
    }
}
