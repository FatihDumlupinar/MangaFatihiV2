using MangaFatihi.Management.WebAPI.Utilities.Providers;
using MangaFatihi.Shared.Utilities.Extensions.Providers;

namespace MangaFatihi.Management.WebAPI.Utilities.Extensions.ServiceProvider
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
