
namespace Umbraco13Test.Services;

public interface IMailService
{
    Task SendAsync(string toEmail, string subject, string messageBody);
}