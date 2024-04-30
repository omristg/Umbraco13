

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco13Test.Controllers;

public class ArticleController : RenderController
{
    private readonly IPublishedValueFallback _publishedValueFallback;

    public ArticleController(ILogger<RenderController> logger,
                             ICompositeViewEngine compositeViewEngine,
                             IUmbracoContextAccessor umbracoContextAccessor,
                             IPublishedValueFallback publishedValueFallback) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _publishedValueFallback = publishedValueFallback;
    }

    public override IActionResult Index()
    {
        var isLoggedIn = Request.HttpContext.User?.Identity?.IsAuthenticated ?? false;
        var loggedInUser = Request.HttpContext.User?.Identity?.Name ?? "";
        var model = new ArticleViewModel(CurrentPage!, _publishedValueFallback)
        {
            CurrentEmail = loggedInUser
        };

        if (isLoggedIn)
        {
            return View("~/Views/Article.cshtml", model);
        }
        return View("~/Views/ArticleFree.cshtml", model);
    }
}