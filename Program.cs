using Microsoft.EntityFrameworkCore;
using Umbraco13Test.Data;
using Umbraco13Test.Repositories;
using Umbraco13Test.Notifications;
using Umbraco.Cms.Core.Notifications;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.CreateUmbracoBuilder()
	.AddBackOffice()
	.AddWebsite()
	.AddDeliveryApi()
	.AddComposers()
	.Build();

builder.Services.AddTransient<IBlogCommentRepository, BlogCommentRepository>();
builder.Services.AddUmbracoDbContext<MyDbContext>(options =>
{
	options.UseSqlServer(config.GetConnectionString("umbracoDbDSN"));
});

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		options.Cookie.HttpOnly = true;
    });


WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
	.WithMiddleware(u =>
	{
		u.UseBackOffice();
		u.UseWebsite();
	})
	.WithEndpoints(u =>
	{
		u.UseInstallerEndpoints();
		u.UseBackOfficeEndpoints();
		u.UseWebsiteEndpoints();
	});

await app.RunAsync();
