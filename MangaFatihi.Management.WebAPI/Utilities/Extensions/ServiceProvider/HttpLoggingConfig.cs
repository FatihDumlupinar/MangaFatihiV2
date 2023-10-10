using Microsoft.AspNetCore.HttpLogging;

namespace MangaFatihi.Management.WebAPI.Utilities.Extensions.ServiceProvider
{
    public static class HttpLoggingConfig
    {
        public static IServiceCollection AddHttpLoggingConfig(this IServiceCollection services)
        {
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                //logging.ResponseHeaders.Add("MyResponseHeader");//eğer geriye döndüğün custom response headers varsa;
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;

            });

            return services;
        }

        public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder builder)
        {
            builder.UseHttpLogging();

            return builder;
        }
    }
}
