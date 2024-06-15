
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Configuration.Models;
using Microsoft.Extensions.Options;

namespace Umbraco13Test.Notifications;

public class ApplicationStartedHandler : INotificationHandler<UmbracoApplicationStartedNotification>
{

    private readonly SmtpSettings? _smtpSettings;

    public ApplicationStartedHandler(IOptions<GlobalSettings> globalSettings)
    {
        _smtpSettings = globalSettings.Value.Smtp;
    }

    public void Handle(UmbracoApplicationStartedNotification notification)
    {
        if (!VerifySmtpSettings(_smtpSettings))
        {
            throw new Exception("Smtp settings must be set in the appsettings.json file");
        }

    }

    private bool VerifySmtpSettings(SmtpSettings? smtpSettings)
    {
        if (smtpSettings is null)
        {
            return false;
        }
        return !string.IsNullOrWhiteSpace(smtpSettings.Host) &&
               smtpSettings.Port > 0 &&
               !string.IsNullOrWhiteSpace(smtpSettings.Username) &&
               !string.IsNullOrWhiteSpace(smtpSettings.Password) &&
               !string.IsNullOrWhiteSpace(smtpSettings.From);
    }
}
