using Microsoft.AspNetCore.Mvc;

namespace LuckyFoodSystem.API.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpPost]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
