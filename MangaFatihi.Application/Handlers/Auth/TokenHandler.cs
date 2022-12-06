using MangaFatihi.Application.Authorize;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MangaFatihi.Application.Handlers.Auth
{
    public class TokenHandler : ITokenHandler
    {
        #region Ctor&Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenHandler(IConfiguration configuration, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        #endregion

        /// <summary>
        /// Token üreten fonksiyon
        /// </summary>
        public async Task<TokenModel> CreateAccessTokenAsync(AppUser appUser, string? ipAddress, CancellationToken cancellationToken = default)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                    new Claim("fullName", appUser.FullName??""),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRoles = await _userManager.GetRolesAsync(appUser);

            List<Claim> permissionClaims = new();
            IList<Claim>? roleClaims = default;
            AppRole? findRole = default;

            foreach (var userRole in userRoles)
            {
                //ClaimTypes.Role çok uzun isim getiriyordu :)
                authClaims.Add(new Claim("role", userRole));

                findRole = await _roleManager.FindByNameAsync(userRole);
                roleClaims = await _roleManager.GetClaimsAsync(findRole);

                //Custom olarak oluşturduğumuz Permission Claim leri JWT nin içine ekliyoruz
                permissionClaims = roleClaims.Where(i => i.Type == CustomClaimTypes.Permission).ToList();
                authClaims.AddRange(permissionClaims);

            }

            var tokenInstance = new TokenModel();

            //Security  Key'in simetriğini alıyoruz.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Secret"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //token süresi
            tokenInstance.Expiration = DateTime.Now.AddDays(7);

            //Oluşturulacak token ayarlarını veriyoruz.
            var securityToken = new JwtSecurityToken(
                    issuer: _configuration["JWTSettings:Issuer"],
                    audience: _configuration["JWTSettings:Audience"],
                    expires: tokenInstance.Expiration,
                    notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
                    signingCredentials: signingCredentials,
                    claims: authClaims
                );

            //Token oluşturucu sınıfında bir örnek alıyoruz.
            var tokenHandler = new JwtSecurityTokenHandler();

            //Token üretiyoruz.
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token veritabanında oluşturuyoruz ve kullanıcıya Id sini döndürüyoruz. Id zaten Guid türünde olduğundan hem Unique hem de arama yapılırken daha hızlı bulunur
            var refreshTokenEntity = await _unitOfWork.RefreshToken.AddAsyncReturnEntity(new()
            {
                AppUser = appUser,
                AccessToken = tokenInstance.AccessToken,
                IpAddress = ipAddress

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            tokenInstance.RefreshToken = refreshTokenEntity.Id.ToString("N");

            return tokenInstance;
        }

    }
}
