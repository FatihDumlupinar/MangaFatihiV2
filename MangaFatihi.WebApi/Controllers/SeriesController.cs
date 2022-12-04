using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Commands;
using MangaFatihi.Models.CQRS.Bindings.Queries;
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
        [ProducesResponseType(typeof(SuccessDataResult<GetListSeriesWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListSeriesWithFilterQuery query, CancellationToken cancellation)
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
        public async ValueTask<IActionResult> GetInformationAsync(string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesInformationQuery() { SeriesId = seriesId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri oluşturan servis
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateSeriesCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seriyi güncelleyen servis
        /// </summary>
        /// <param name="seriesId">Serinin Unique Id'si</param>
        [HttpPut("{seriesId}")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditSeriesCommand command, string seriesId, CancellationToken cancellation)
        {
            command.SeriesId = seriesId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seriyi silen servis
        /// </summary>
        /// <param name="seriesId">Serinin Unique Id'si</param>
        [HttpDelete("{seriesId}")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesCommand() { SeriesId = seriesId }, cancellation);
            return CustomStandartReturnAction(result);
        }


    }
}
