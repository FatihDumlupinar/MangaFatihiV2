using MangaFatihi.Application.Extensions;
using MangaFatihi.Application.Seed;
using MangaFatihi.Persistence.Extensions;
using MangaFatihi.WebApi.Utilities.Extensions;
using MangaFatihi.WebApi.Utilities.Handlers;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddKestrelMaxRequestBodySizeConfig();

var config = AppsettingsConfig.AddAppsettingsConfig();

builder.UseCustomSerilogConfig();

builder.Services.AddHttpLoggingConfig();

builder.Services.AddDbContextConfig(config);

builder.Services.AddApplicationDependecyConfig();

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

//builder.Services.AddAuthorization(options =>
//{

//    #region Settings

//    options.AddPolicy(PolicyTypes.Claims_Settings.List, policy => policy.RequireClaim(PolicyTypes.CustomClaimTypes.Permission, PolicyTypes.Claims_Settings.List));
//    options.AddPolicy(PolicyTypes.Claims_Settings.Read, policy => policy.RequireClaim(PolicyTypes.CustomClaimTypes.Permission, PolicyTypes.Claims_Settings.Read));
//    options.AddPolicy(PolicyTypes.Claims_Settings.Update, policy => policy.RequireClaim(PolicyTypes.CustomClaimTypes.Permission, PolicyTypes.Claims_Settings.Update));
//    options.AddPolicy(PolicyTypes.Claims_Settings.Delete, policy => policy.RequireClaim(PolicyTypes.CustomClaimTypes.Permission, PolicyTypes.Claims_Settings.Delete));

//    #endregion
//});

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
