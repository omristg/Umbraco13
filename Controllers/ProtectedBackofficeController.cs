using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace Umbraco13Test.Controllers;


public class ProtectedBackofficeController : UmbracoAuthorizedApiController
{
    // All methods in this controller are protected with backoffice user access
    // path is /umbraco/backoffice/api/ProtectedBackoffice/GetProduct
    [HttpGet]
    public IActionResult GetProduct()
    {
        return Content("Products");
    }
}