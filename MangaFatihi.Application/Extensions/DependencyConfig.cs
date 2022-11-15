using MangaFatihi.Application.Handlers.Auth;
using MangaFatihi.Application.Repositories;
using MangaFatihi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Application.Extensions
{
    public static class DependencyConfig
    {
        /// <summary>
        /// Uygulama içinde kullanılan dependency ler
        /// </summary>
        public static IServiceCollection AddDependecyConfig(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITokenHandler, TokenHandler>();

            return services;
        }
    }
}
