using MangaFatihi.Application.CQRS.Commands;
using MangaFatihi.Application.CQRS.Queries;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Commands;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Serilerin CRUD işlemlerinin yapıldığı Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : CustomBaseController<SeriesController>
    {
        /// <summary>
        /// Filtreye göre serileri getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [ProducesResponseType(typeof(SuccessDataResult<SeriesGetListWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListWithFilterAsync(SeriesGetListWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan serinin detaylı bilgisini getiren servis
        /// </summary>
        /// <param name="seriesId">Serinin Unique Id'si</param>
        [HttpGet("{seriesId}")]
        [ProducesResponseType(typeof(SuccessDataResult<GetSeriesInformationQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSeriesInformationAsync(string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesInformationQuery() { SeriesId = seriesId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri oluşturan servis
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataResult<CreateSeriesCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeriesAsync(CreateSeriesCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seriyi güncelleyen servis
        /// </summary>
        /// <param name="seriesId">Serinin Unique Id'si</param>
        [HttpPut("{seriesId}")]
        [ProducesResponseType(typeof(SuccessDataResult<EditSeriesCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditSeriesAsync(EditSeriesCommand command, string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seriyi silen servis
        /// </summary>
        /// <param name="seriesId">Serinin Unique Id'si</param>
        [HttpDelete("{seriesId}")]
        [ProducesResponseType(typeof(SuccessDataResult<DeleteSeriesCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeriesAsync(string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesCommand() { SeriesId = seriesId }, cancellation);
            return CustomStandartReturnAction(result);
        }


    }
}
