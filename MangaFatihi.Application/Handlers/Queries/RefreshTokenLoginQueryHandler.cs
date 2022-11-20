using MangaFatihi.Application.Constants;
using MangaFatihi.Application.CQRS.Queries;
using MangaFatihi.Application.Handlers.Auth;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MangaFatihi.Application.Handlers.Queries
{
    public class RefreshTokenLoginQueryHandler : IRequestHandler<RefreshTokenLoginQuery, DataResult<RefreshTokenLoginQueryDto>>
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

        public async Task<DataResult<RefreshTokenLoginQueryDto>> Handle(RefreshTokenLoginQuery request, CancellationToken cancellationToken)
        {
            var returnModel = new RefreshTokenLoginQueryDto();

            var refreshToken = Guid.Parse(request.RefreshToken);

            var refreshTokenEntity = _unitOfWork.RefreshToken.GetById(refreshToken);
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

            returnModel.RefreshToken = token.RefreshToken;
            returnModel.AccessToken = token.AccessToken;
            returnModel.Expiration = token.Expiration;

            return new SuccessDataResult<RefreshTokenLoginQueryDto>(returnModel, ApplicationMessages.SuccessRefreshTokenLogin.GetMessage(), ApplicationMessages.SuccessRefreshTokenLogin);
        }
    }
}
