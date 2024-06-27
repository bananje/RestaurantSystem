using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;
using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Contracts;
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISender sender)
        {
            _logger = logger;
            _SENDER = sender;
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
            List<OrderLineRequestStruct> orderLines = [];

            var o = new OrderLineRequestStruct(Guid.Parse("0e482dd0-258f-4172-9da3-9eadb11a7c03"), 34);

            orderLines.Add(o);

            var command = new CreateOrderCommand(Guid.Parse("957b692b-d1d0-4b8e-a0ee-64e06cab3abb"), "fgerg", "gdfg", "gfdfg","34", orderLines);

            var t = await _SENDER.Send(command);

            return Ok();

        }
    }
}
