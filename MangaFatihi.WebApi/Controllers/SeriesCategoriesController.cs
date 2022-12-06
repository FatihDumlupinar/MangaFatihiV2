using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Seri kategorilerin bilgilerinin CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesCategoriesController : CustomBaseController<SeriesCategoriesController>
    {
        /// <summary>
        /// Filtreye göre Seri kategorilerini getiren servis
        /// </summary>
        [HttpPost("get-list-with-filter")]
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
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteAsync(string seriesCategoryId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteSeriesCategoryCommand() { SeriesCategoryId = seriesCategoryId }, cancellation);
            return CustomStandartReturnAction(result);
        }
    }
}
