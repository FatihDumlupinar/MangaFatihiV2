using Microsoft.AspNetCore.Http.Features;

namespace MangaFatihi.WebApi.Utilities.Extensions.ServiceProvider
{
    public static class UploadFileSizeLimitConfig
    {
        private const long MAX_FILE_SIZE_LIMIT = int.MaxValue;//2Gb

        public static WebApplicationBuilder AddKestrelMaxRequestBodySizeConfig(this WebApplicationBuilder builder)
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = MAX_FILE_SIZE_LIMIT;
            });

            return builder;
        }

        public static IServiceCollection AddFormOptionsMultipartBodyLengthLimitConfig(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = MAX_FILE_SIZE_LIMIT;
            });

            return services;
        }

    }
}
