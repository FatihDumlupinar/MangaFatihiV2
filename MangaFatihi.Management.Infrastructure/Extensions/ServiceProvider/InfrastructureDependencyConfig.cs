using MangaFatihi.Management.Infrastructure.Services.SeriesEpisode;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Management.Infrastructure.Extensions.ServiceProvider
{
    public static class InfrastructureDependencyConfig
    {
        /// <summary>
        /// Infrastructure katmanında kullanılan dependency ler
        /// </summary>
        public static IServiceCollection AddInfrastructureDependencyConfig(this IServiceCollection services)
        {
            services.AddSingleton<ISeriesEpisodeFileService, SeriesEpisodeFileService>();

            return services;
        }
    }
}
