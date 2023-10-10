using MangaFatihi.Management.Application.Repositories;
using MangaFatihi.Management.Application.Handlers.Auth;
using MangaFatihi.Shared.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Management.Application.Extensions
{
    public static class ApplicationDependencyConfig
    {
        /// <summary>
        /// Application katmanında kullanılan dependency ler
        /// </summary>
        public static IServiceCollection AddApplicationDependecyConfig(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITokenHandler, TokenHandler>();

            return services;
        }
    }
}
