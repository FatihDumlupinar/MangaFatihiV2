using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Seri yazarların bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesAuthorsController : CustomBaseController<SeriesAuthorsController>
    {
        /// <summary>
        /// Filtreye göre Seri yazarlarını getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
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
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesAuthorId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesAuthorCommand() { SeriesAuthorId = seriesAuthorId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
