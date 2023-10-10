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
    /// Seri bölümlerin bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.List)]
    [ApiController]
    public class SeriesEpisodesController : CustomBaseController<SeriesEpisodesController>
    {
        /// <summary>
        /// Filtreye göre Seri bölümlerini getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.List)]
        [ProducesResponseType(typeof(SuccessDataResult<GetListSeriesEpisodesWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListSeriesEpisodesWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri bölümlerinin hangi seriler ile ilişkili olduğunun detaylı bilgisini getiren servis 
        /// </summary>
        /// <param name="seriesEpisodeId">Seri bölümünün Unique Id'si</param>
        [HttpGet("{seriesEpisodeId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.Read)]
        [ProducesResponseType(typeof(SuccessDataResult<GetSeriesEpisodeInformationQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetInformationAsync(string seriesEpisodeId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesEpisodeInformationQuery() { SeriesEpisodeId = seriesEpisodeId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri bölümü oluşturan servis
        /// </summary>
        [HttpPost]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.Create)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateSeriesEpisodeCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri bölümünü güncelleyen servis
        /// </summary>
        /// <param name="seriesEpisodeId">Seri bölümünün Unique Id'si</param>
        [HttpPut("{seriesEpisodeId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.Update)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditSeriesEpisodeCommand command, string seriesEpisodeId, CancellationToken cancellation)
        {
            command.Id = seriesEpisodeId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri bölümünü silen servis
        /// </summary>
        /// <param name="seriesEpisodeId">Seri bölümünün Unique Id'si</param>
        [HttpDelete("{seriesEpisodeId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisode.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesEpisodeId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesEpisodeCommand() { SeriesEpisodeId = seriesEpisodeId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        //todo Çoklu bölüm ekleme servisi yapılacak

    }
}
