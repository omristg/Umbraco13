using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.PublishedCache;

namespace Umbraco13Test.Notifications;

public class PrintArticleOptions : INotificationHandler<ContentPublishingNotification>
{
    private readonly IContentService _contentService;
    private readonly UmbracoHelper _umbracoHelper;
    private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;


    public PrintArticleOptions(IContentService contentService, UmbracoHelper umbracoHelper, IPublishedSnapshotAccessor publishedSnapshotAccessor)
    {
        _contentService = contentService;
        _umbracoHelper = umbracoHelper;
        _publishedSnapshotAccessor = publishedSnapshotAccessor;
    }

    public void Handle(ContentPublishingNotification notification)
    {
        foreach (var node in notification.PublishedEntities)
        {
            if (node.ContentType.Alias == Article.ModelTypeAlias)
            {
                // Example for using published snapshot accessor
                var udi = node.GetValue(Article.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Options)!.Alias);
                // Example for using udi
                var test = _umbracoHelper.Content(udi!);
                System.Console.WriteLine(test?.Name);
            }
        }
    }
}
