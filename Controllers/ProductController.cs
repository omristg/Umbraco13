using Microsoft.AspNetCore.Mvc;

namespace Umbraco13Test.Controllers;

// Example for usage with different action routing options
[ApiController]
[Route("api/[controller]")]
[EndpointGroupName("Products")]
public class ProductsController : ControllerBase
{
    [HttpGet("GetProducts")]
    public IActionResult GetAll() => Ok(new[] { "Table", "Chair", "Desk", "Computer" });

    [HttpGet]
    [Route("/api2/different/route")]
    public IActionResult DifferentRoute() => Ok(new[] { "Table", "Chair", "Desk", "Computer", "TV" });
}