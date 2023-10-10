using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Authorize.Policies;
using MangaFatihi.Shared.Domain.Entities.Statics;
using MangaFatihi.Shared.Models.Enms;
using MangaFatihi.Shared.Persistance.Context;
using MangaFatihi.Shared.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace MangaFatihi.Management.Application.Seed
{
    public static class SeedData
    {
        public static async ValueTask EnsurePopulatedAsync(this IServiceProvider services)
        {
            using var cancelTokenSource = new CancellationTokenSource();

            using var scope = services.CreateScope();

            using var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            await writeDbContext.Database.MigrateAsync(cancelTokenSource.Token);

            #region Statik Bilgiler

            if (!await writeDbContext.StaticSeriesStatus.AnyAsync(cancelTokenSource.Token))
            {
                await writeDbContext.StaticSeriesStatus.AddRangeAsync(new List<StaticSeriesStatus>()
                {
                    new() { Id=(int)StaticSeriesStatusEnm.ongoing, Name=EnumHelper<StaticSeriesStatusEnm>.GetDisplayValue(StaticSeriesStatusEnm.ongoing) },
                    new() { Id=(int)StaticSeriesStatusEnm.stopped, Name=EnumHelper<StaticSeriesStatusEnm>.GetDisplayValue(StaticSeriesStatusEnm.stopped) },
                    new() { Id=(int)StaticSeriesStatusEnm.finished, Name=EnumHelper<StaticSeriesStatusEnm>.GetDisplayValue(StaticSeriesStatusEnm.finished) },
                    new() { Id=(int)StaticSeriesStatusEnm.cancelled, Name=EnumHelper<StaticSeriesStatusEnm>.GetDisplayValue(StaticSeriesStatusEnm.cancelled) },

                }, cancelTokenSource.Token);

                await writeDbContext.SaveChangesAsync(cancelTokenSource.Token);

            }

            if (!await writeDbContext.StaticSeriesTypes.AnyAsync(cancelTokenSource.Token))
            {
                await writeDbContext.StaticSeriesTypes.AddRangeAsync(new List<StaticSeriesType>()
                {
                    new(){Id=(int)StaticSeriesTypeEnm.manga,Name=EnumHelper<StaticSeriesTypeEnm>.GetDisplayValue(StaticSeriesTypeEnm.manga) },
                    new(){Id=(int)StaticSeriesTypeEnm.webtoon,Name=EnumHelper<StaticSeriesTypeEnm>.GetDisplayValue(StaticSeriesTypeEnm.webtoon) },
                    new(){Id=(int)StaticSeriesTypeEnm.manhua,Name=EnumHelper<StaticSeriesTypeEnm>.GetDisplayValue(StaticSeriesTypeEnm.manhua) },
                    new(){Id=(int)StaticSeriesTypeEnm.manhwa,Name=EnumHelper<StaticSeriesTypeEnm>.GetDisplayValue(StaticSeriesTypeEnm.manhwa) },
                    new(){Id=(int)StaticSeriesTypeEnm.novel,Name=EnumHelper<StaticSeriesTypeEnm>.GetDisplayValue(StaticSeriesTypeEnm.novel) },

                }, cancelTokenSource.Token);

                await writeDbContext.SaveChangesAsync(cancelTokenSource.Token);
            }

            if (!await writeDbContext.StaticSeriesEpisodeTypes.AnyAsync(cancelTokenSource.Token))
            {
                await writeDbContext.StaticSeriesEpisodeTypes.AddRangeAsync(new List<StaticSeriesEpisodeType>()
                {
                    new(){Id=(int)StaticSeriesEpisodeTypeEnm.image,Name=EnumHelper<StaticSeriesEpisodeTypeEnm>.GetDisplayValue(StaticSeriesEpisodeTypeEnm.image) },
                    new(){Id=(int)StaticSeriesEpisodeTypeEnm.text,Name=EnumHelper<StaticSeriesEpisodeTypeEnm>.GetDisplayValue(StaticSeriesEpisodeTypeEnm.text) },

                }, cancelTokenSource.Token);

                await writeDbContext.SaveChangesAsync(cancelTokenSource.Token);
            }

            #endregion

            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            #region Role

            #region Admin

            if (!await roleManager.RoleExistsAsync(EnumHelper<StaticRolesEnm>.GetDisplayValue(StaticRolesEnm.Admin)))
            {
                var role = new AppRole();
                role.Name = EnumHelper<StaticRolesEnm>.GetDisplayValue(StaticRolesEnm.Admin);
                var roleResult = await roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    //user
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppUser.Delete));

                    //role
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claims_AppRole.Delete));

                    //series
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Series.Delete));

                    //series episode
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Delete));

                    //series and series artist
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesArtist.Delete));

                    //series and series author
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesAuthor.Delete));

                    //series and series category
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAndSeriesCategory.Delete));

                    //series artist
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesArtist.Delete));

                    //series author
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesAuthor.Delete));

                    //series category
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesCategory.Delete));

                    //series episode
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisode.Delete));

                    //series episodes page
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_SeriesEpisodesPage.Delete));

                    //team
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_Team.Delete));

                    //team and appuser
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Create));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Update));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Read));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.List));
                    await roleManager.AddClaimAsync(role, new Claim(PolicyTypes.PermissionClaimTypeName, PolicyTypes.Claim_TeamAndAppUser.Delete));




                }
            }

            #endregion

            #endregion

            #region User

            #region Admin

            if (await userManager.FindByEmailAsync("admin@email.com") == null)
            {
                var user = new AppUser();
                user.UserName = "adminTest06";
                user.Email = "admin@email.com";
                user.FullName = "Admin Test";
                user.Gender = 1;
                user.IsActive = true;

                var result = await userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, EnumHelper<StaticRolesEnm>.GetDisplayValue(StaticRolesEnm.Admin));
                }

            }

            #endregion



            #endregion


            cancelTokenSource.Cancel();
        }

    }
}
