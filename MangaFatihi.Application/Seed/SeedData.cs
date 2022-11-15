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

            var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            await writeDbContext.Database.MigrateAsync(cancelTokenSource.Token);

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

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
