using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MangaFatihi.Identity.Application.Seed
{
    public static class SeedData
    {
        public static async Task EnsurePopulatedAsync(this IServiceProvider services)
        {
            using var cancelTokenSource = new CancellationTokenSource();

            using var serviceScope = services.CreateScope();

            using var context = services.GetRequiredService<ConfigurationDbContext>();

#if DEBUG
            var mvcMainUrl = "https://localhost:7006";

#else
            var mvcMainUrl = "https://mangafatihi.com";

#endif

            if (!context.Clients.Any())
            {
                await context.AddRangeAsync(new List<IdentityServer4.EntityFramework.Entities.Client>()
                {
                   new IdentityServer4.Models.Client
                   {
                       //İstemcinin benzersiz kimliğini tanımlar.
                        ClientId = "MangaFatihiWebUI",

                        //İstemcinin adını tanımlar
                        ClientName = "MangaFatihi.WebUI",

                        //İstemcinin kullanabileceği yetkilendirme türlerini tanımlar
                        AllowedGrantTypes =
                        {
                           //Implicit (Örtülü) : İstemci, kullanıcıyı IdentityServer4'e yönlendirir ve ardından kullanıcı oturum açar ve erişim jetonunu alır
                           OidcConstants.GrantTypes.Implicit
                        },

                        // İstemcinin kimlik doğrulama sonrası yönlendirme URI'lerini tanımlar
                        RedirectUris = { $"{mvcMainUrl}/signin-oidc" },

                        //İstemcinin oturum kapatma sonrası yönlendirme URI'lerini tanımlar
                        PostLogoutRedirectUris = { $"{mvcMainUrl}/signout-callback-oidc" },

                        // İstemcinin erişim izni alabileceği kaynakları (kapsamları) tanımlar.
                        //hangi scope değerlerine sahipse ancak o scope değerlerine sahip olan API’lara istekte bulunabilir.
                        AllowedScopes =
                        {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           "MangaFatihi.Identity.WebAPI.ReadWrite",
                           "MangaFatihi.Management.WebAPI.ReadWrite",
                           "MangaFatihi.Reader.WebAPI.ReadWrite",
                           "MangaFatihi.ApiGateway.WebAPI.ReadWrite",

                        }
                   }.ToEntity(),

                   new IdentityServer4.Models.Client
                   {
                        ClientId = "MangaFatihiIdentityWebAPI",
                        ClientName = "MangaFatihi.Identity.WebAPI",
                        AllowedGrantTypes =
                       { 
                           //İstemci, kimlik bilgilerini kullanarak doğrudan IdentityServer4'ten erişim jetonu alır. Kullanıcı doğrudan dahil olmaz. 
                           OidcConstants.GrantTypes.ClientCredentials
                       },
                        //ClientSecrets : İstemci kimlik doğrulama işlemi sırasında bu gizli bilgiyi kullanarak IdentityServer4'e kendini tanıtarak erişim jetonu (access token) alabilir. Genellikle 
                        ClientSecrets = { new Secret("clientsecret_MangaFatihiIdentityWebAPI".ToSha256()) },
                        AllowedScopes =
                        {
                           "MangaFatihi.Identity.WebAPI.ReadWrite",
                           "MangaFatihi.Management.WebAPI.ReadWrite",
                           "MangaFatihi.Reader.WebAPI.ReadWrite",
                           "MangaFatihi.ApiGateway.WebAPI.ReadWrite",

                        }
                   }.ToEntity(),

                   new IdentityServer4.Models.Client
                   {
                        ClientId = "MangaFatihiManagementWebAPI",
                        ClientName = "MangaFatihi.Management.WebAPI",
                        AllowedGrantTypes ={ OidcConstants.GrantTypes.ClientCredentials },
                        ClientSecrets = { new Secret("clientsecret_MangaFatihiManagementWebAPI".ToSha256()) },
                        AllowedScopes =
                        {
                           "MangaFatihi.Identity.WebAPI.ReadWrite",
                           "MangaFatihi.Management.WebAPI.ReadWrite",
                           "MangaFatihi.Reader.WebAPI.ReadWrite",
                           "MangaFatihi.ApiGateway.WebAPI.ReadWrite",

                        }
                   }.ToEntity(),

                   new IdentityServer4.Models.Client
                   {
                        ClientId = "MangaFatihiReaderWebAPI",
                        ClientName = "MangaFatihi.Reader.WebAPI",
                        AllowedGrantTypes ={ OidcConstants.GrantTypes.ClientCredentials },
                        ClientSecrets = { new Secret("clientsecret_MangaFatihiReaderWebAPI".ToSha256()) },
                        AllowedScopes =
                        {
                           "MangaFatihi.Identity.WebAPI.ReadWrite",
                           "MangaFatihi.Management.WebAPI.ReadWrite",
                           "MangaFatihi.Reader.WebAPI.ReadWrite",
                           "MangaFatihi.ApiGateway.WebAPI.ReadWrite",

                        }
                   }.ToEntity(),

                   new IdentityServer4.Models.Client
                   {
                        ClientId = "MangaFatihiApiGatewayWebAPI",
                        ClientName = "MangaFatihi.ApiGateway.WebAPI",
                        AllowedGrantTypes ={ OidcConstants.GrantTypes.ClientCredentials },
                        ClientSecrets = { new Secret("clientsecret_MangaFatihiApiGatewayWebAPI".ToSha256()) },
                        AllowedScopes =
                        {
                           "MangaFatihi.Identity.WebAPI.ReadWrite",
                           "MangaFatihi.Management.WebAPI.ReadWrite",
                           "MangaFatihi.Reader.WebAPI.ReadWrite",
                           "MangaFatihi.ApiGateway.WebAPI.ReadWrite",

                        }
                   }.ToEntity(),

                }, cancelTokenSource.Token);

                await context.SaveChangesAsync(cancelTokenSource.Token);
            }

            if (!context.ApiScopes.Any())
            {
                await context.ApiScopes.AddRangeAsync(new List<IdentityServer4.EntityFramework.Entities.ApiScope>()
                {
                    new IdentityServer4.Models.ApiScope("MangaFatihi.Identity.WebAPI.ReadWrite").ToEntity(),
                    new IdentityServer4.Models.ApiScope("MangaFatihi.Management.WebAPI.ReadWrite").ToEntity(),
                    new IdentityServer4.Models.ApiScope("MangaFatihi.Reader.WebAPI.ReadWrite").ToEntity(),
                    new IdentityServer4.Models.ApiScope("MangaFatihi.ApiGateway.WebAPI.ReadWrite").ToEntity(),

                });

                await context.SaveChangesAsync(cancelTokenSource.Token);
            }

            if (!context.ApiResources.Any())
            {
                await context.ApiResources.AddRangeAsync(new List<IdentityServer4.EntityFramework.Entities.ApiResource>()
                {
                    new IdentityServer4.Models.ApiResource("MangaFatihi.Identity.WebAPI")
                    {
                        Scopes = { "MangaFatihi.Identity.WebAPI.ReadWrite" }
                    }.ToEntity(),
                    new IdentityServer4.Models.ApiResource("MangaFatihi.Management.WebAPI")
                    {
                        Scopes = { "MangaFatihi.Management.WebAPI.ReadWrite" }
                    }.ToEntity(),
                    new IdentityServer4.Models.ApiResource("MangaFatihi.Reader.WebAPI")
                    {
                        Scopes = { "MangaFatihi.Reader.WebAPI.ReadWrite" }
                    }.ToEntity(),
                    new IdentityServer4.Models.ApiResource("MangaFatihi.ApiGateway.WebAPI")
                    {
                        Scopes = { "MangaFatihi.ApiGateway.WebAPI.ReadWrite" }
                    }.ToEntity(),
                }, cancelTokenSource.Token);
            }

            if (!context.IdentityResources.Any())
            {
                await context.IdentityResources.AddRangeAsync(new List<IdentityServer4.EntityFramework.Entities.IdentityResource>()
                {
                    new IdentityServer4.Models.IdentityResources.OpenId().ToEntity(),
                    new IdentityServer4.Models.IdentityResources.Profile().ToEntity(),
                    new IdentityResource {
                        Name = "PositionAndAuthority",
                        DisplayName = "Position And Authority",
                        Description = "Kullanıcı pozisyonu ve yetkisi.",
                        UserClaims = { "position", "authority" }
                    }.ToEntity(),
                    new IdentityResource {
                        Name = "Roles",
                        DisplayName = "Roles",
                        Description = "Kullanıcı rolleri",
                        UserClaims = { "role" }
                    }.ToEntity(),
                    new IdentityResource {
                        Name = "UserInfo",
                        DisplayName = "User Info",
                        Description = "Kullanıcı bilgileri",
                        UserClaims = { "name", "website" }
                    }.ToEntity()
                }, cancelTokenSource.Token);
            }


            cancelTokenSource.Cancel();
        }

    }
}
