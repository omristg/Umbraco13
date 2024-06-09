
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

public class CorsComposer : IComposer
{
    public const string AllowAnyOriginPolicyName = nameof(AllowAnyOriginPolicyName);

    public void Compose(IUmbracoBuilder builder)
    {
        // Can also use AllowAnyOrigin method
        builder.Services
       .AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()))
       .Configure<UmbracoPipelineOptions>(options =>
            options.AddFilter(new UmbracoPipelineFilter("Cors", postRouting: app => app.UseCors())));
    }

}