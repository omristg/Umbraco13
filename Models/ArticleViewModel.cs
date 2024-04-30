using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco.Cms.Web.Common.PublishedModels;

public class ArticleViewModel : Article
{
    public ArticleViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
        : base(content, publishedValueFallback)
    {

    }
    public string CurrentEmail { get; set; } = string.Empty;
}