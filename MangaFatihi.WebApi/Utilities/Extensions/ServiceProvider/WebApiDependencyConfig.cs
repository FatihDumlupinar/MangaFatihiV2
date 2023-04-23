using MangaFatihi.Infrastructure.Providers;
using MangaFatihi.WebApi.Utilities.Providers;

namespace MangaFatihi.WebApi.Utilities.Extensions.ServiceProvider
{
    public static class WebApiDependencyConfig
    {
        /// <summary>
        /// WebApi katmanında kullanılan dependency ler
        /// </summary>
        public static IServiceCollection AddWebApiDependecyConfig(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSingleton<IStaticFileDirectoryProvider, StaticFileDirectoryProvider>();

            return services;
        }
    }
}
