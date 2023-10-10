﻿using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.Identity.WebAPI.Utilities.Extensions.ServiceProviders
{
    public static class ControllersConfig
    {
        public static IServiceCollection AddControllersConfig(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new GlobalValidationFilter());
            }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}