using MangaFatihi.Application.CQRS.Queries;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : CustomBaseController<AccountController>
    {

        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessDataResult<UserLoginQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync(UserLoginQuery query, CancellationToken cancellation)
        {
            query.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        [HttpPost("refresh-token-login")]
        [ProducesResponseType(typeof(SuccessDataResult<RefreshTokenLoginQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshTokenLoginAsync(RefreshTokenLoginQuery query, CancellationToken cancellation)
        {
            query.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

    }
}
