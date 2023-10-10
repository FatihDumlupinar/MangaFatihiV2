using MangaFatihi.Identity.Application.Seed;
using MangaFatihi.Identity.WebAPI.Utilities.Extensions.ServiceProviders;
using MangaFatihi.Identity.WebAPI.Utilities.Handlers;
using MangaFatihi.Shared.Appsettings;
using MangaFatihi.Shared.Authorize.Extensions.ServiceProvider;
using MangaFatihi.Shared.Models.Extensions.ServiceProviders;
using MangaFatihi.Shared.Persistance.Extensions;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

var sharedConfig = SharedAppsettingsConfig.AddSharedAppsettingsConfig(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

var config = AppsettingsConfig.AddAppsettingsConfig();

builder.UseCustomSerilogConfig();

builder.Services.AddHttpLoggingConfig();

builder.Services.AddDbContextConfig(sharedConfig);

//builder.Services.AddApplicationDependecyConfig();

//builder.Services.AddInfrastructureDependencyConfig();

builder.Services.AddWebApiDependecyConfig();

builder.Services.AddControllersConfig();

builder.Services.AddIdentityConfig();

builder.Services.AddIdentityServerConfig(sharedConfig);

builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;//identity içindeki hatalarý göster
}

builder.Services.AddCorsConfig();

builder.Services.AddSwaggerConfig();

#pragma warning disable CS0612 // Type or member is obsolete
builder.Services.AddFluentValidationConfig();
#pragma warning restore CS0612 // Type or member is obsolete

builder.Services.AddJwtTokenAuthentication(config);

builder.Services.AddCustomAuthorization();

var app = builder.Build();

app.UseCustomHttpLogging();

app.UseCustomExceptionHandler();

app.UseCustomSwaggerSetup();

app.UseCustomCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.EnsurePopulatedAsync();

app.Run();
