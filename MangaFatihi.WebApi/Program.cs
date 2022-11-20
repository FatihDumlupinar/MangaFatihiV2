using MangaFatihi.Application.Extensions;
using MangaFatihi.Application.Seed;
using MangaFatihi.Persistence.Extensions;
using MangaFatihi.WebApi.Extensions;
using MangaFatihi.WebApi.Handlers;

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

builder.Services.AddMediatRConfig();

builder.Services.AddDbContextConfig(config);

builder.Services.AddDependecyConfig();

builder.Services.AddControllersConfig();

builder.Services.AddIdentityConfig();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddJwtTokenAuthentication(config);

builder.Services.AddSwaggerConfig();

builder.Services.AddEndpointsApiExplorer();

#pragma warning disable CS0612 // Type or member is obsolete
builder.Services.AddFluentValidationConfig();
#pragma warning restore CS0612 // Type or member is obsolete

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseCustomSwaggerSetup();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.EnsurePopulatedAsync();

app.Run();
