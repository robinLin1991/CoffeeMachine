using CoffeeMachineAPI.Domain.Enums;
using CoffeeMachineAPI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeMachineService _coffeeMachineService;

        public CoffeeController(ICoffeeMachineService coffeeMachineService)
        {
            _coffeeMachineService = coffeeMachineService;
        }

        [HttpGet("brew-coffee")]
        public async Task<IActionResult> BrewCoffee()
        {
            try
            {
                var result = await _coffeeMachineService.BrewCoffeeAsync();

                //return 200 OK with corresponding message and prepared time
                return Ok(new { message = result.message, prepared = result.prepared.ToString("o") });
            }
            catch (InvalidOperationException ex)
            {
                //rerurn 418 I’m a teapot
                if (ex.Message == StatusResults.Teapot)
                {
                    return StatusCode(418);
                    
                }
                //return 503 Service Unavailable
                else if (ex.Message == StatusResults.ServiceUnavailable)
                {
                    return StatusCode(503);
                }

                return StatusCode(500);
            }
        }
    }
}
