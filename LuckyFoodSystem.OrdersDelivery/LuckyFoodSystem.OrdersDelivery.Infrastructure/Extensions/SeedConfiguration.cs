using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.Common;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using LuckyFoodSystem.Shared.Domain.Models.Entity;
using LuckyFoodSystem.Shared.Domain.Models.ValueObjects;
using Marten;
using Newtonsoft.Json;
using Npgsql;
using System.Text;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.Extensions;

public class SeedConfiguration
{
    public static void EnsureDatabase(string connectionString, string databaseName)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var adminConnectionString = $"Host={builder.Host};Port={builder.Port};Username={builder.Username};Password={builder.Password}";

        using var connection = new NpgsqlConnection(adminConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
        var exists = command.ExecuteScalar() != null;

        if (!exists)
        {
            command.CommandText = $"CREATE DATABASE \"{databaseName}\"";
            command.ExecuteNonQuery();
        }
    }

    public static void SeedDataAsync(IDocumentStore documentStore)
    {
        using var session = documentStore.LightweightSession();

        // Создание событий
        var customerId = new CustomerId(Guid.NewGuid());
        var customerCreatedEvent = new CustomerCreatedEvent(
            customerId,
            "John",
            "Doe",
            "Smith",
            new Email("john.doe@example.com"),
            new Phone("+1234567890"),
            0,
            new Address("fd45345","fd34535","fd4543","545"));

        var orderId = new OrderId(Guid.NewGuid());
        var address = new Address("dfFEWF", "fdWEFWEF", "fdWEFWEF", "33");
        var order = new Order(customerId, address); // Дополните инициализацию Order, если необходимо
        var orderConfirmedEvent = new OrderConfirmedEvent(order);

        // Начальная вставка данных
        session.Events.StartStream(typeof(Customer), customerId.Value, SerializeEvent(customerCreatedEvent));
        // session.Events.StartStream(typeof(Order), orderId.Value, SerializeEvent(orderConfirmedEvent));

        session.SaveChanges();

    }

    private static byte[] SerializeEvent(IDomainEvent @event)
    {
        var eventType = @event.GetType().AssemblyQualifiedName;
        var eventData = new
        {
            EventType = eventType,
            Data = @event
        };

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventData, settings));
    }

    private static IDomainEvent DeserializeEvent(ReadOnlyMemory<byte> data)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        var eventData = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(data.ToArray()), settings);
        string type = eventData.EventType;
        string eventJson = eventData.Data.ToString();
        return (IDomainEvent)JsonConvert.DeserializeObject(eventJson, Type.GetType(type)!, settings)!;
    }
}
