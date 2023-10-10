using MangaFatihi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Identity.WebAPI.Utilities.Extensions.ServiceProviders
{
    public static class IdentityServerConfig
    {
        public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddIdentityServer()
                //AddOperationalStore : IdentityServer4'ün çalışma zamanı verilerini (örneğin, oturumlar ve revokasyonlar) veritabanında depolamak için kullanılır
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                    options.EnableTokenCleanup = true;
                })
                //AddConfigurationStore : IdentityServer4 ile yapılandırma bilgilerini (configuration data) veritabanında depolamak için kullanılır
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                })
                //AddAspNetIdentity : ASP.NET Identity ile entegrasyonu yapılandırmak için kullanılır
                .AddAspNetIdentity<AppUser>();

            return services;
        }
    }
}
