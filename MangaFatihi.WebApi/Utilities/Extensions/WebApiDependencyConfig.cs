using MangaFatihi.WebApi.Utilities.Services;

namespace MangaFatihi.WebApi.Utilities.Extensions
{
    public static class WebApiDependencyConfig
    {
        /// <summary>
        /// WebApi katmanında kullanılan dependency ler
        /// </summary>
        public static IServiceCollection AddWebApiDependecyConfig(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
