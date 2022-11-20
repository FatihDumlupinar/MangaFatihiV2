using FluentValidation.AspNetCore;
using MangaFatihi.Application.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Application.Extensions
{
    public static class FluentValidationConfig
    {
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
