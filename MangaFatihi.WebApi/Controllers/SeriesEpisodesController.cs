using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Seri bölümlerin bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesEpisodesController : CustomBaseController<SeriesEpisodesController>
    {
        /// <summary>
        /// Filtreye göre Seri bölümlerini getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
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
