using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Application.Extensions
{
    public static class MediatorConfig
    {
        /// <summary>
        /// d
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
