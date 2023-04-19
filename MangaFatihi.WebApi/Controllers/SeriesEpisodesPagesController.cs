using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using MangaFatihi.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers
{
    /// <summary>
    /// Seri bölümleri sayfaların CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesEpisodesPagesController : CustomBaseController<SeriesEpisodesPagesController>
    {
        /// <summary>
        /// Toplu resim ekleyen ve geriye dönen servis
        /// </summary>
        [HttpPost("{seriesEpisodeId}/MultiUploadImages")]
        [ProducesResponseType(typeof(SuccessDataResult<MultiUploadSeriesEpisodeImagesCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> MultiUploadImagesAsync([FromRoute] Guid seriesEpisodeId, [FromForm] SeriesEpisodesMultiUploadImagesModel binding, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new MultiUploadSeriesEpisodeImagesCommand() { Files = binding.Files, SeriesEpisodeId = seriesEpisodeId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Tekli resim ekleyen ve geriye dönen servis
        /// </summary>
        /// <returns></returns>
        [HttpPost("{seriesEpisodeId}/UploadImage")]
        [ProducesResponseType(typeof(SuccessDataResult<UploadSeriesEpisodeImageCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> UploadImageAsync([FromRoute] Guid seriesEpisodeId, [FromForm] UploadImageModel binding, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new UploadSeriesEpisodeImageCommand() { File = binding.File, SeriesEpisodeId = seriesEpisodeId }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Toplu novel(yazı) sayfaları ekleyen servis
        /// </summary>
        [HttpPost("CreateMultiNovelPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateMultiNovelPageAsync(CreateMultiNovelPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Tekli novel(yazı) sayfaları ekleyen servis
        /// </summary>
        [HttpPost("CreateOneNovelPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateOneNovelPageAsync(CreateOneNovelPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Toplu novel(yazı) sayfaları güncelleyen servis
        /// </summary>
        [HttpPut("UpdateMultiNovelPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> UpdateMultiNovelPageAsync(UpdateMultiNovelPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Tekli novel(yazı) sayfaları güncelleyen servis
        /// </summary>
        [HttpPut("UpdateOneNovelPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> UpdateOneNovelPageAsync(UpdateOneNovelPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Toplu (Novel,Seri,vs hepsi için) sayfaları silen servis
        /// </summary>
        [HttpPost("DeleteMultiPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteMultiPageAsync(DeleteMultiPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Tekli (Novel,Seri,vs hepsi için) sayfaları silen servis
        /// </summary>
        [HttpDelete("{seriesPageId}")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteOnePageAsync(string seriesPageId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteOnePageCommand() { SeriesPageId = seriesPageId }, cancellation);
            return CustomStandartReturnAction(result);
        }

    }
}
