using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.CreateOrder;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;
using LuckyFoodSystem.Orders.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuckyFoodSystem.Orders.API.Controllers
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

            var command = new CreateOrderCommand(Guid.Parse("1044ba55-c13f-46d1-a49a-c4cf7f7206d3"), "fgerg", "gdfg", "gfdfg","34", orderLines);

            var t = await _SENDER.Send(command);

            return Ok();

        }
    }
}
