using MangaFatihi.Management.WebAPI.Controllers.Base;
using MangaFatihi.Shared.Authorize.Policies;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.DataResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.Management.WebAPI.Controllers
{
    /// <summary>
    /// Seri sanatçıları bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.List)]
    [ApiController]
    public class SeriesArtistsController : CustomBaseController<SeriesArtistsController>
    {
        /// <summary>
        /// Filtreye göre Seri sanatçılarını getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.List)]
        [ProducesResponseType(typeof(SuccessDataResult<GetListSeriesArtistsWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListSeriesArtistsWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri sanatçısının hangi seriler ile ilişkili olduğunun detaylı bilgisini getiren servis 
        /// </summary>
        /// <param name="seriesArtistId">Seri Sanatçısının Unique Id'si</param>
        [HttpGet("{seriesArtistId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.Read)]
        [ProducesResponseType(typeof(SuccessDataResult<GetSeriesArtistInformationQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetInformationAsync(string seriesArtistId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesArtistInformationQuery() { SeriesArtistId = seriesArtistId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri sanatçısı oluşturan servis
        /// </summary>
        [HttpPost]
        [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.Create)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateSeriesArtistCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri sanatçısını güncelleyen servis
        /// </summary>
        /// <param name="seriesArtistId">Seri Sanatçısının Unique Id'si</param>
        [HttpPut("{seriesArtistId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.Update)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditSeriesArtistCommand command, string seriesArtistId, CancellationToken cancellation)
        {
            command.Id = seriesArtistId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri sanatçısını silen servis
        /// </summary>
        /// <param name="seriesArtistId">Seri Sanatçısının Unique Id'si</param>
        [HttpDelete("{seriesArtistId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesArtist.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesArtistId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesArtistCommand() { SeriesArtistId = seriesArtistId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
