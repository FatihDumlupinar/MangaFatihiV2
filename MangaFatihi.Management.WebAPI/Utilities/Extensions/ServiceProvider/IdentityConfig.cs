using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Persistance.Context;
using Microsoft.AspNetCore.Identity;

namespace MangaFatihi.Management.WebAPI.Utilities.Extensions.ServiceProvider
{
    public static class IdentityConfig
    {
        /// <summary>
        /// Identity kurulumu ve ayarları
        /// </summary>
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // User settings
                options.User.AllowedUserNameCharacters = "abcçdefgğhiıjklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;//eposta adresi eşsiz olacak

                // Password settings
                options.Password.RequireDigit = false;//şifre belirlerken; sayı zorunluğu yok
                options.Password.RequiredLength = 6;//şifre belirlerken;min 6 karakter olacak
                options.Password.RequiredUniqueChars = 0;//şifre belirlerken;eşsiz karakter zorunluluğu yok 
                options.Password.RequireLowercase = false;//şifre belirlerken;küçük harf zorunluluğu yok
                options.Password.RequireUppercase = false;//şifre belirlerken;büyük harf zorunluluğu yok
                options.Password.RequireNonAlphanumeric = false;//şifre belirlerken;özel karakter zorunluluğu yok

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            })
            .AddEntityFrameworkStores<WriteDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
