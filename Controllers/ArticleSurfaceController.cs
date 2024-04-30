using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Umbraco13Test.Controllers;
public class ArticleSurfaceController : SurfaceController
{

    private readonly IPublishedContentQuery _query;
    public ArticleSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
                                 IUmbracoDatabaseFactory databaseFactory,
                                 ServiceContext services,
                                 AppCaches appCaches,
                                 IProfilingLogger profilingLogger,
                                 IPublishedUrlProvider publishedUrlProvider,
                                 IPublishedContentQuery query) : base(umbracoContextAccessor,
                                                            databaseFactory,
                                                            services,
                                                            appCaches,
                                                            profilingLogger,
                                                            publishedUrlProvider)
    {
        _query = query;
    }

    [HttpPost]
    public IActionResult Search(string? query)
    {
        Console.WriteLine(query);
        ViewBag.Results = _query.Search($"{query}*");


        return PartialView("SearchResult");
    }
}
