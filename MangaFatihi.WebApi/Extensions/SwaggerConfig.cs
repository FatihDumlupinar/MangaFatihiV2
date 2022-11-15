using MangaFatihi.WebApi.Filters;
using Microsoft.OpenApi.Models;

namespace MangaFatihi.WebApi.Extensions
{
    public static class SwaggerConfig
    {
        /// <summary>
        /// Swagger kurulumu ve ayarları
        /// </summary>
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MangaFatihi.WebApi", Version = "v1", Description = "MangaFatihi WebApi" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });

                c.SchemaFilter<HideParametersSwaggerSchemaFilter>();
            });
        }

        /// <summary>
        /// Swagger kurulumu ve UI ayarları
        /// </summary>
        public static void UseCustomSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
       
    }
}
