namespace MangaFatihi.Identity.WebAPI.Utilities.Extensions.ServiceProviders
{
    public static class CorsConfig
    {
        private const string POLICY_NAME = "CustomCors_MangaFatihi_IdenTity";

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>

                options.AddPolicy(
                    POLICY_NAME,
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
