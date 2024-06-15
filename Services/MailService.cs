using MailKit.Net.Smtp;
using MimeKit;
using Umbraco.Cms.Core.Configuration.Models;
using Microsoft.Extensions.Options;

namespace Umbraco13Test.Services;

public class MailService : IMailService
{
    private readonly GlobalSettings _globalSettings;
    public MailService(IOptions<GlobalSettings> globalSettings)
    {
        _globalSettings = globalSettings.Value;
    }

    public async Task SendAsync(string toEmail, string subject, string messageBody)
    {

        var smtpConfig = _globalSettings.Smtp;
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(smtpConfig!.From, smtpConfig.Username));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html") { Text = messageBody };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpConfig.Host, smtpConfig.Port, false);
            await client.AuthenticateAsync(smtpConfig.Username, smtpConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

}