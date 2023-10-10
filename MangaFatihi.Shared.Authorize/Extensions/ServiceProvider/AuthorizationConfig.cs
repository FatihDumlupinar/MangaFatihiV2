
using MangaFatihi.Shared.Authorize.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Shared.Authorize.Extensions.ServiceProvider
{
    public static class AuthorizationConfig
    {
        /// <summary>
        /// Bütün uygulama içinde kullanılacak yetkilendirmelerin tanımlandığı yerdir.
        /// </summary>
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //appuser
                options.AddPolicy(PolicyTypes.Claims_AppUser.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Create));
                options.AddPolicy(PolicyTypes.Claims_AppUser.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.List));
                options.AddPolicy(PolicyTypes.Claims_AppUser.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Read));
                options.AddPolicy(PolicyTypes.Claims_AppUser.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Update));
                options.AddPolicy(PolicyTypes.Claims_AppUser.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Delete));

                //approle
                options.AddPolicy(PolicyTypes.Claims_AppRole.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Create));
                options.AddPolicy(PolicyTypes.Claims_AppRole.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.List));
                options.AddPolicy(PolicyTypes.Claims_AppRole.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Read));
                options.AddPolicy(PolicyTypes.Claims_AppRole.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Update));
                options.AddPolicy(PolicyTypes.Claims_AppRole.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Delete));

                //series
                options.AddPolicy(PolicyTypes.Claim_Series.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Create));
                options.AddPolicy(PolicyTypes.Claim_Series.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.List));
                options.AddPolicy(PolicyTypes.Claim_Series.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Read));
                options.AddPolicy(PolicyTypes.Claim_Series.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Update));
                options.AddPolicy(PolicyTypes.Claim_Series.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Delete));

                //series series artist
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesArtist.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesArtist.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesArtist.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesArtist.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesArtist.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Delete));

                //series series author
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesAuthor.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesAuthor.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesAuthor.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesAuthor.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesAuthor.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Delete));

                //series series category
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesCategory.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesCategory.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesCategory.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesCategory.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesAndSeriesCategory.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Delete));

                //series artist
                options.AddPolicy(PolicyTypes.Claim_SeriesArtist.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesArtist.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesArtist.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesArtist.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesArtist.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Delete));

                //series author
                options.AddPolicy(PolicyTypes.Claim_SeriesAuthor.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesAuthor.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesAuthor.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesAuthor.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesAuthor.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Delete));

                //series category
                options.AddPolicy(PolicyTypes.Claim_SeriesCategory.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesCategory.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesCategory.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesCategory.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesCategory.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Delete));

                //series episode
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisode.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisode.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisode.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisode.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisode.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Delete));

                //series episode page
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisodesPage.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Create));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisodesPage.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.List));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisodesPage.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Read));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisodesPage.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Update));
                options.AddPolicy(PolicyTypes.Claim_SeriesEpisodesPage.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Delete));

                //team
                options.AddPolicy(PolicyTypes.Claim_Team.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Create));
                options.AddPolicy(PolicyTypes.Claim_Team.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.List));
                options.AddPolicy(PolicyTypes.Claim_Team.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Read));
                options.AddPolicy(PolicyTypes.Claim_Team.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Update));
                options.AddPolicy(PolicyTypes.Claim_Team.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Delete));

                //team and appuser
                options.AddPolicy(PolicyTypes.Claim_TeamAndAppUser.Create, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Create));
                options.AddPolicy(PolicyTypes.Claim_TeamAndAppUser.List, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.List));
                options.AddPolicy(PolicyTypes.Claim_TeamAndAppUser.Read, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Read));
                options.AddPolicy(PolicyTypes.Claim_TeamAndAppUser.Update, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Update));
                options.AddPolicy(PolicyTypes.Claim_TeamAndAppUser.Delete, policy => policy.RequireClaim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Delete));

            });

            return services;
        }

    }

}
