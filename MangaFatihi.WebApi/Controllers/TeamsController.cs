using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Kullanıcı ekibi CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : CustomBaseController<TeamsController>
    {
        /// <summary>
        /// Filtreye göre Ekipleri getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [ProducesResponseType(typeof(SuccessDataResult<GetListTeamsWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListTeamsWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan Ekiplerin detaylı bilgisini getiren servis 
        /// </summary>
        /// <param name="teamId">Ekiplerin Unique Id'si</param>
        [HttpGet("{teamId}")]
        [ProducesResponseType(typeof(SuccessDataResult<GetTeamInformationQuery>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetInformationAsync(string teamId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetTeamInformationQuery() { TeamId = teamId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni Ekip oluşturan servis
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateTeamCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan Ekipleri güncelleyen servis
        /// </summary>
        /// <param name="teamId">Ekiplerin Unique Id'si</param>
        [HttpPut("{teamId}")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditTeamCommand command, string teamId, CancellationToken cancellation)
        {
            command.TeamId = teamId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan Ekipleri silen servis
        /// </summary>
        /// <param name="teamId">Ekiplerin Unique Id'si</param>
        [HttpDelete("{teamId}")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string teamId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteTeamCommand() { TeamId = teamId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
