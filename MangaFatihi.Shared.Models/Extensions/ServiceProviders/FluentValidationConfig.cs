using FluentValidation.AspNetCore;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Shared.Models.Extensions.ServiceProviders
{
    public static class FluentValidationConfig
    {
        /// <summary>
        /// FluentValidation için gerekli ayarları yapar.
        /// </summary>
        [Obsolete]
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddFluentValidation(options =>
            {
                // Validate child properties and root collection elements
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;

                options.RegisterValidatorsFromAssemblyContaining<RefreshTokenLoginQueryValidator>();
            });

            return services;
        }
    }
}
