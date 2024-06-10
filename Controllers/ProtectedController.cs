using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Umbraco.Cms.Web.Common.Filters;

namespace Umbraco13Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    // Protected with backoffice user access
    [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    [HttpGet("GetProduct")]
    public IActionResult GetProduct()
    {
        return Content("Products");
    }

    // Protected with member login, with "Basic" members group
    [UmbracoMemberAuthorize("", "Basic", "")]
    [HttpGet("Test")]
    public IActionResult Test()
    {
        System.Console.WriteLine("here");
        return Ok("Products");
    }

}