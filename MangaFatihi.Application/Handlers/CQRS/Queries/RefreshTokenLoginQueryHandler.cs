using MangaFatihi.Application.Handlers.Auth;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
{
    public class RefreshTokenLoginQueryHandler : IQueryHandler<RefreshTokenLoginQuery, DataResult<RefreshTokenLoginQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public RefreshTokenLoginQueryHandler(IUnitOfWork unitOfWork, ITokenHandler tokenHandler, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _userManager = userManager;
        }

        #endregion

        public async ValueTask<DataResult<RefreshTokenLoginQueryDto>> Handle(RefreshTokenLoginQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = Guid.Parse(request.RefreshToken);

            var refreshTokenEntity = await _unitOfWork.RefreshToken.GetByIdAsync(refreshToken, cancellationToken);
            if (refreshTokenEntity == null)
            {
                return new ErrorDataResult<RefreshTokenLoginQueryDto>(ApplicationMessages.ErrorLoginRefreshTokenNotFound.GetMessage(), ApplicationMessages.ErrorLoginRefreshTokenNotFound);
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(refreshTokenEntity.AccessToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return new ErrorDataResult<RefreshTokenLoginQueryDto>(ApplicationMessages.ErrorLoginRefreshTokenInvalidToken.GetMessage(), ApplicationMessages.ErrorLoginRefreshTokenInvalidToken);
            }

            string username = principal.Identity?.Name ?? "";//boş dönmez de, genede warning vermesin

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ErrorDataResult<RefreshTokenLoginQueryDto>(ApplicationMessages.ErrorLoginUserNotFound.GetMessage(), ApplicationMessages.ErrorLoginUserNotFound);
            }

            var token = await _tokenHandler.CreateAccessTokenAsync(user, request.IpAddress, cancellationToken);

            var returnModel = new RefreshTokenLoginQueryDto();

            returnModel.RefreshToken = token.RefreshToken;
            returnModel.AccessToken = token.AccessToken;
            returnModel.Expiration = token.Expiration;

            return new SuccessDataResult<RefreshTokenLoginQueryDto>(returnModel, ApplicationMessages.SuccessRefreshTokenLogin.GetMessage(), ApplicationMessages.SuccessRefreshTokenLogin);
        }
    }
}
