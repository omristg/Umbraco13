
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco13Test.Models;

public class CorsComposer : IComposer
{
    public const string AllowAnyOriginPolicyName = nameof(AllowAnyOriginPolicyName);
    public void Compose(IUmbracoBuilder builder)
    {
        var allowedOrigins = builder.Config.GetSection(nameof(CorsSettings)).Get<CorsSettings>()?.AllowedOrigins;
        if (allowedOrigins is null || allowedOrigins.Length == 0)
        {
            throw new Exception("AllowedOrigins must be set in the appsettings.json file");
        }

        // Can also use AllowAnyOrigin method
        builder.Services
       .AddCors(options => options.AddDefaultPolicy(policy =>
            policy
                .WithOrigins(allowedOrigins!)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()))
       .Configure<UmbracoPipelineOptions>(options =>
            options.AddFilter(new UmbracoPipelineFilter("Cors", postRouting: app => app.UseCors())));
    }

}