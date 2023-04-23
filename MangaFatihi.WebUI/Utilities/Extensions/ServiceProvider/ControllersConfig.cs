using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Utilities.Extensions.ServiceProvider
{
    public static class ControllersConfig
    {
        public static IServiceCollection AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
