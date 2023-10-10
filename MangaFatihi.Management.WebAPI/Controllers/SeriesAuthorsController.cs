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
    /// Seri yazarların bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.List)]
    [ApiController]
    public class SeriesAuthorsController : CustomBaseController<SeriesAuthorsController>
    {
        /// <summary>
        /// Filtreye göre Seri yazarlarını getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.List)]
        [ProducesResponseType(typeof(SuccessDataResult<GetListSeriesAuthorsWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListSeriesAuthorsWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri yazarısının hangi seriler ile ilişkili olduğunun detaylı bilgisini getiren servis 
        /// </summary>
        /// <param name="seriesAuthorId">Seri yazarının Unique Id'si</param>
        [HttpGet("{seriesAuthorId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.Read)]
        [ProducesResponseType(typeof(SuccessDataResult<GetSeriesAuthorInformationQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetInformationAsync(string seriesAuthorId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesAuthorInformationQuery() { SeriesAuthorId = seriesAuthorId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri yazarını oluşturan servis
        /// </summary>
        [HttpPost]
        [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.Create)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateSeriesAuthorCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri yazarını güncelleyen servis
        /// </summary>
        /// <param name="seriesAuthorId">Seri yazarının Unique Id'si</param>
        [HttpPut("{seriesAuthorId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.Update)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditSeriesAuthorCommand command, string seriesAuthorId, CancellationToken cancellation)
        {
            command.Id = seriesAuthorId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri yazarını silen servis
        /// </summary>
        /// <param name="seriesAuthorId">Seri yazarının Unique Id'si</param>
        [HttpDelete("{seriesAuthorId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesAuthor.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesAuthorId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesAuthorCommand() { SeriesAuthorId = seriesAuthorId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
