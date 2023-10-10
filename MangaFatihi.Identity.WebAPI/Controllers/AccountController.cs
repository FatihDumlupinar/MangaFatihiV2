using MangaFatihi.Identity.WebAPI.Controllers.Base;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.DataResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.Identity.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : CustomBaseController<AccountController>
    {
        /// <summary>
        /// Kullanıcı girişi
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessDataResult<UserLoginQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> LoginAsync(UserLoginQuery query, CancellationToken cancellation)
        {
            query.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Refresh token ile kullanıcı girişi yapan servis
        /// </summary>
        [HttpPost("refresh-token-login")]
        [ProducesResponseType(typeof(SuccessDataResult<RefreshTokenLoginQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> RefreshTokenLoginAsync(RefreshTokenLoginQuery query, CancellationToken cancellation)
        {
            query.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

    }
}
