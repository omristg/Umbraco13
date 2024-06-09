using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Umbraco.Cms.Web.Common.Filters;

namespace Umbraco13Test.Controllers;

public class ProtectedController : UmbracoApiController
{
    // Protected with backoffice user access
    [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    [HttpGet]
    public IActionResult GetProduct()
    {
        return Content("Products");
    }

    // Protected with member login, with "Basic" members group
    [UmbracoMemberAuthorize("", "Basic", "")]
    [HttpGet]
    public IActionResult Test()
    {
        System.Console.WriteLine("here");
        return Ok("Products");
    }

}