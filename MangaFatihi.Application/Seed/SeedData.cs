using MangaFatihi.Application.Authorize;
using MangaFatihi.Domain.Enms;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Infrastructure.Helpers;
using MangaFatihi.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using static MangaFatihi.Application.Authorize.PolicyTypes;

namespace MangaFatihi.Application.Seed
{
    public static class SeedData
    {
        public static async Task EnsurePopulatedAsync(this IServiceProvider services)
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
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Claims_User.Create));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Claims_User.Delete));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Claims_User.List));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Claims_User.Read));
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Claims_User.Update));


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
