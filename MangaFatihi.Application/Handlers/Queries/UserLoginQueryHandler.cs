using MangaFatihi.Application.Constants;
using MangaFatihi.Application.CQRS.Queries;
using MangaFatihi.Application.Handlers.Auth;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MangaFatihi.Application.Handlers.Queries
{
    internal class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, DataResult<UserLoginQueryDto>>
    {
        #region Ctor&Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenHandler _tokenHandler;

        public UserLoginQueryHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenHandler = tokenHandler;
        }

        #endregion

        public async Task<DataResult<UserLoginQueryDto>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var returnModel = new UserLoginQueryDto();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new ErrorDataResult<UserLoginQueryDto>(ApplicationMessages.ErrorLoginUserNotFound.GetMessage(), ApplicationMessages.ErrorLoginUserNotFound);
            }

            var token = await _tokenHandler.CreateAccessTokenAsync(user, request.IpAddress, cancellationToken);

            returnModel.RefreshToken = token.RefreshToken;
            returnModel.AccessToken = token.AccessToken;
            returnModel.Expiration = token.Expiration;

            return new SuccessDataResult<UserLoginQueryDto>(returnModel, ApplicationMessages.SuccessLogin.GetMessage(), ApplicationMessages.SuccessLogin);
        }
    }
}
