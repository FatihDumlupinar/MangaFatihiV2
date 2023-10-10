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
    /// Serilerin CRUD işlemlerinin yapıldığı Controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_Series.List)]
    [ApiController]
    public class SeriesController : CustomBaseController<SeriesController>
    {
        /// <summary>
        /// Filtreye göre serileri getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [Authorize(Policy = PolicyTypes.Claim_Series.List)]
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
        [Authorize(Policy = PolicyTypes.Claim_Series.Read)]
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
        [Authorize(Policy = PolicyTypes.Claim_Series.Create)]
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
        [Authorize(Policy = PolicyTypes.Claim_Series.Update)]
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
        [Authorize(Policy = PolicyTypes.Claim_Series.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesCommand() { SeriesId = seriesId }, cancellation);
            return CustomStandartReturnAction(result);
        }


    }
}
