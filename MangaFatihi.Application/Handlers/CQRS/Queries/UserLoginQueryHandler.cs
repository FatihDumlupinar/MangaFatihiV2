using MangaFatihi.Application.Handlers.Auth;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Queries;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
{
    public class UserLoginQueryHandler : IQueryHandler<UserLoginQuery, DataResult<UserLoginQueryDto>>
    {
        #region Ctor&Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public UserLoginQueryHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        #endregion

        public async ValueTask<DataResult<UserLoginQueryDto>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
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
