using Microsoft.AspNetCore.Mvc;

namespace Umbraco13Test.Controllers;

// Example for usage with different action routing options
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("{action}")]
    public IActionResult GetAll() => Ok(new[] { "Table", "Chair", "Desk", "Computer" });

    [HttpGet("GetProducts")]
    public IActionResult DifferentActionName() => Ok(new[] { "Table", "Chair", "Desk", "Computer", "TV" });

    [HttpGet]
    [Route("/api2/different/route")]
    public IActionResult FullDifferentRoute() => Ok(new[] { "Table", "Chair", "Desk", "Computer", "TV" });
}