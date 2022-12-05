using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace MangaFatihi.WebApi.Handlers
{
    public static class ExceptionHandler
    {
        /// <summary>
        /// Uygulama da hata oluşursa; hata mesajını loglayıp, kullanıcıya uygun bir cevap döndüren ara servis (ExceptionHandler)
        /// </summary>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {

            builder.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var logger = context.RequestServices.GetService<ILogger>();
                        if (logger != default)
                        {
                            logger.LogError(error.Error, "CustomExceptionHandler Error : ");
                        }
                    }

                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(
                            new DataResult<object>(null, 500, ApplicationMessages.ErrorDefaultExceptionHandler.GetMessage(), ApplicationMessages.ErrorDefaultExceptionHandler)
                            ));
                });
            });
            return builder;
        }
    }
}
