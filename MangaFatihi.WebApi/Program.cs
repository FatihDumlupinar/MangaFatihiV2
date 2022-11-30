using MangaFatihi.Application.Extensions;
using MangaFatihi.Application.Seed;
using MangaFatihi.Persistence.Extensions;
using MangaFatihi.WebApi.Extensions;
using MangaFatihi.WebApi.Handlers;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using System.Diagnostics;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

#region Appsettings

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)

//eðer buradaki json file ý bulursa yukarýdakini okumaz, yoksa yukarýdakine bakar
.AddJsonFile($"appsettings.{env}.json", optional: true)
.AddEnvironmentVariables();

var config = configuration.Build();

#endregion

builder.Host.UseSerilog();

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg)); Serilog.Debugging.SelfLog.Enable(Console.Error);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    //logging.ResponseHeaders.Add("MyResponseHeader");//eðer geriye döndüðün custom response headers varsa;
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

builder.Services.AddDbContextConfig(config);

builder.Services.AddDependecyConfig();

builder.Services.AddControllersConfig();

builder.Services.AddMediatrConfig();

builder.Services.AddIdentityConfig();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddJwtTokenAuthentication(config);

builder.Services.AddSwaggerConfig();

builder.Services.AddEndpointsApiExplorer();

#pragma warning disable CS0612 // Type or member is obsolete
builder.Services.AddFluentValidationConfig();
#pragma warning restore CS0612 // Type or member is obsolete

var app = builder.Build();

app.UseHttpLogging();

app.UseCustomExceptionHandler();

app.UseCustomSwaggerSetup();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.EnsurePopulatedAsync();

app.Run();
