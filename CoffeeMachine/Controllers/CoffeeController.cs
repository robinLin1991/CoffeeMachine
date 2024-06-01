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
                return Ok(new { message = result.message, prepared = result.prepared.ToString("o") });
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == StatusResults.Teapot)
                {
                    return StatusCode(418);
                    
                }
                else if (ex.Message == StatusResults.ServiceUnavailable)
                {
                    return StatusCode(503);
                }

                return StatusCode(500);
            }
        }
    }
}
