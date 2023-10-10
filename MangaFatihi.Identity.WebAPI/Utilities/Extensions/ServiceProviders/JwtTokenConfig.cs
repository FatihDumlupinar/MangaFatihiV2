using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MangaFatihi.Identity.WebAPI.Utilities.Extensions.ServiceProviders
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
                    //todo : authority eklenecek
                    //x.Authority = configuration["JWTSettings:Authority"];

                    //1 ise true, değilse false, bu durum Realese de true olacaktır Debug da false
                    x.RequireHttpsMetadata = configuration["JWTSettings:RequireHttpsMetadata"] == "1";

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Secret"])),

                        ValidateLifetime = true,

                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidateIssuer = true,

                        ValidAudience = configuration["JWTSettings:Audience"],
                        ValidateAudience = true,

                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero,
                    };

                    x.SaveToken = true;

                });

            return services;
        }
    }
}
