using MangaFatihi.Management.Application.Extensions;
using MangaFatihi.Management.Application.Seed;
using MangaFatihi.Management.Infrastructure.Extensions.ServiceProvider;
using MangaFatihi.Management.WebAPI.Utilities.Extensions.ServiceProvider;
using MangaFatihi.Management.WebAPI.Utilities.Handlers;
using MangaFatihi.Shared.Authorize.Extensions.ServiceProvider;
using MangaFatihi.Shared.Models.Extensions.ServiceProviders;
using MangaFatihi.Shared.Persistance.Extensions;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddKestrelMaxRequestBodySizeConfig();

var config = AppsettingsConfig.AddAppsettingsConfig();

builder.UseCustomSerilogConfig();

builder.Services.AddHttpLoggingConfig();

builder.Services.AddDbContextConfig(config);

builder.Services.AddApplicationDependecyConfig();

builder.Services.AddInfrastructureDependencyConfig();

builder.Services.AddWebApiDependecyConfig();

builder.Services.AddControllersConfig();

builder.Services.AddFormOptionsMultipartBodyLengthLimitConfig();

builder.Services.AddMediatrConfig();

builder.Services.AddIdentityConfig();

if (builder.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;//identity içindeki hatalarý göster
}

builder.Services.AddCorsConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddEndpointsApiExplorer();

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
