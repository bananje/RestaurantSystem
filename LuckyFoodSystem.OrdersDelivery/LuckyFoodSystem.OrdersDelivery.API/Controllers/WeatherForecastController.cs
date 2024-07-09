using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;
using LuckyFoodSystem.Shared.Contracts.Common.DTO;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuckyFoodSystem.OrdersDelivery.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ISender _SENDER;

        private readonly IPublishEndpoint _publishEndpoint;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISender sender, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _SENDER = sender;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                await _publishEndpoint.Publish<OrderCreated>(new
                {
                    CustomerId = Guid.NewGuid(),
                    OrderLines = new List<OrderLineStruct>()
                });

                var confirmedOrder
            }
            catch
            {

            }
            List<OrderLineRequestStruct> orderLines = [];

            var o = new OrderLineRequestStruct(Guid.Parse("0e482dd0-258f-4172-9da3-9eadb11a7c03"), 34);

            orderLines.Add(o);

            var command = new CreateOrderCommand(Guid.Parse("957b692b-d1d0-4b8e-a0ee-64e06cab3abb"), "fgerg", "gdfg", "gfdfg","34", orderLines);

            var t = await _SENDER.Send(command);

            return Ok();

        }
    }
}
