using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MangaFatihi.Application.Extensions
{
    public static class MediatRConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
