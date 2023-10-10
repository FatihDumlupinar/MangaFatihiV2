using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Management.Application.Extensions
{
    public static class MediatorConfig
    {
        /// <summary>
        /// Mediator ayarları
        /// </summary>
        public static IServiceCollection AddMediatrConfig(this IServiceCollection services)
        {
            services.AddMediator(options =>
            {
                options.Namespace = "MangaFatihi.Application";
                options.ServiceLifetime= ServiceLifetime.Scoped;
            });


            return services;
        }
    }
}
