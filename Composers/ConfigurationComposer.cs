using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Configuration.Models;

public class ConfigurationComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        var globalSettings = builder.Config.GetSection("Umbraco:CMS:Global");
        builder.Services.Configure<GlobalSettings>(globalSettings);
    }
}