using LuckyFoodRestaurantSystem.Contracts.Api;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersDeliveryController : ControllerBase
    {
        private readonly ISender _sender;

        public OrdersDeliveryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> PostCreateOrder([FromBody][Required] CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(request.CustomerId, request.OrderLines);

            var result = await _sender.Send(command);

            return Ok();

        }
    }
}
