namespace MangaFatihi.Management.WebAPI.Utilities.Extensions.ServiceProvider
{
    public static class CorsConfig
    {
        private const string POLICY_NAME = "CustomCors_MangaFatihi";

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>

                options.AddPolicy(
                    "CustomCors_",
                    p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                )
            );

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder builder)
        {
            builder.UseCors(POLICY_NAME);

            return builder;
        }
    }
}
