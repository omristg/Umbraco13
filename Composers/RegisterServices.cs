using Umbraco.Cms.Core.Composing;
using Umbraco13Test.Services;

public class RegisterServices : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<IMailService, MailService>();
    }
}
