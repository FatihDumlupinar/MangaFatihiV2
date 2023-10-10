using MangaFatihi.Management.WebAPI.Controllers.Base;
using MangaFatihi.Shared.Authorize.Policies;
using MangaFatihi.Shared.Models.API.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.DataResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.Management.WebAPI.Controllers
{
    /// <summary>
    /// Seri kategorilerin bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.List)]
    [ApiController]
    public class SeriesCategoriesController : CustomBaseController<SeriesCategoriesController>
    {
        /// <summary>
        /// Filtreye göre Seri kategorilerini getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.List)]
        [ProducesResponseType(typeof(SuccessDataResult<GetListSeriesCategoriesWithFilterQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetListWithFilterAsync(GetListSeriesCategoriesWithFilterQuery query, CancellationToken cancellation)
        {
            var result = await Mediator.Send(query, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri kategorisini hangi seriler ile ilişkili olduğunun detaylı bilgisini getiren servis 
        /// </summary>
        /// <param name="seriesCategoryId">Seri yazarının Unique Id'si</param>
        [HttpGet("{seriesCategoryId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.Read)]
        [ProducesResponseType(typeof(SuccessDataResult<GetSeriesCategoryInformationQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> GetInformationAsync(string seriesCategoryId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new GetSeriesCategoryInformationQuery() { SeriesCategoryId = seriesCategoryId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Yeni seri kategorisi oluşturan servis
        /// </summary>
        [HttpPost]
        [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.Create)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateAsync(CreateSeriesCategoryCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri kategorisini güncelleyen servis
        /// </summary>
        /// <param name="seriesCategoryId">Seri yazarının Unique Id'si</param>
        [HttpPut("{seriesCategoryId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.Update)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> EditAsync(EditSeriesCategoryCommand command, string seriesCategoryId, CancellationToken cancellation)
        {
            command.Id = seriesCategoryId;
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Varolan seri kategorisini silen servis
        /// </summary>
        /// <param name="seriesCategoryId">Seri yazarının Unique Id'si</param>
        [HttpDelete("{seriesCategoryId}")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesCategory.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesCategoryId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesCategoryCommand() { SeriesCategoryId = seriesCategoryId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
