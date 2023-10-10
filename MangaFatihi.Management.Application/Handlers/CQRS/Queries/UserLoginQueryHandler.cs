using MangaFatihi.Domain.Entities;
using MangaFatihi.Management.Application.Handlers.Auth;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
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
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new ErrorDataResult<UserLoginQueryDto>(ApplicationMessages.ErrorLoginUserNotFound.GetMessage(), ApplicationMessages.ErrorLoginUserNotFound);
            }

            var token = await _tokenHandler.CreateAccessTokenAsync(user, request.IpAddress, cancellationToken);

            var returnModel = new UserLoginQueryDto();
            returnModel.RefreshToken = token.RefreshToken;
            returnModel.AccessToken = token.AccessToken;
            returnModel.Expiration = token.Expiration;

            return new SuccessDataResult<UserLoginQueryDto>(returnModel, ApplicationMessages.SuccessLogin.GetMessage(), ApplicationMessages.SuccessLogin);
        }
    }
}
