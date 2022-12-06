using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Seri sanatçıları bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesArtistsController : CustomBaseController<SeriesArtistsController>
    {
        /// <summary>
        /// Filtreye göre Seri sanatçılarını getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
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
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesArtistId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesArtistCommand() { SeriesArtistId = seriesArtistId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
