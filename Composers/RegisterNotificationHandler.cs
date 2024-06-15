using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
using Umbraco13Test.Notifications;

public class RegisterNotificationHandler : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder
            .AddNotificationHandler<UmbracoApplicationStartedNotification, ApplicationStartedHandler>()
            .AddNotificationHandler<ContentPublishingNotification, PrintArticleOptions>();


    }
}