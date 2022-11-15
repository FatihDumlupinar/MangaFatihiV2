using MangaFatihi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Persistence.Extensions
{
    public static class DbContextConfigure
    {
        /// <summary>
        /// DbContext ayarları ve veritabanı bağlantısı
        /// </summary>
        public static IServiceCollection AddDbContextConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            //sadece yazma, değiştirme yetkisi olan veritabanı bağlantısı ve onun için kullanılacak olan DbContext
            services.AddDbContext<WriteDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WriteDbConnection"));
            });

            //sadece okuma yetkisi olan veritabanı bağlantısı ve onun için kullanılacak olan DbContext
            services.AddDbContext<ReadOnlyDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("ReadOnlyDbConnection"));
            });

            return services;
        }

    }
}
