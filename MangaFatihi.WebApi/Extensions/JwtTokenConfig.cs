using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace MangaFatihi.WebApi.Extensions
{
    public static class JwtTokenConfig
    {
        /// <summary>
        /// Jwt kurulumu ve ayarları
        /// </summary>
        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
                .AddJwtBearer(x =>
                {
                    x.Authority = configuration["JWTSettings:Authority"];

                    //1 ise true, değilse false, bu durum Realese de true olacaktır Debug da false
                    x.RequireHttpsMetadata = configuration["JWTSettings:RequireHttpsMetadata"] == "1";

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],

                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero,
                    };

                    x.SaveToken = true;

                    x.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";

                            var logger = c.HttpContext.RequestServices.GetService<ILogger>();
                            if (logger != default)
                            {
                                logger.LogError(c.Exception, "Jwt oluşumunda hata, OnAuthenticationFailed");
                            }

                            return c.Response.WriteAsync(JsonConvert.SerializeObject(
                                    new DataResult<object>(null, 500, ApplicationMessages.ErrorJWTAuthenticationFailed.GetMessage(), ApplicationMessages.ErrorJWTAuthenticationFailed)
                                ));
                        },
                        OnChallenge = context =>
                        {
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                context.HandleResponse();

                                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                                   new DataResult<object>(null, 401, ApplicationMessages.ErrorJWTNotAuthorized.GetMessage(), ApplicationMessages.ErrorJWTNotAuthorized)
                                ));
                            }
                            else
                            {
                                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                                   new DataResult<object>(null, 400, ApplicationMessages.ErrorJWTTokenExpired.GetMessage(), ApplicationMessages.ErrorJWTTokenExpired)
                                ));
                            }
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";

                            return context.Response.WriteAsync(JsonConvert.SerializeObject(
                                   new DataResult<object>(null, 400, ApplicationMessages.ErrorJWTForbidden.GetMessage(), ApplicationMessages.ErrorJWTForbidden)
                                ));
                        },
                    };
                });

            return services;
        }
    }
}
