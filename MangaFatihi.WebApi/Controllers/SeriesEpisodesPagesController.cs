using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using MangaFatihi.WebApi.Controllers.Base;
using MangaFatihi.WebApi.Utilities.Services;
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
        #region Ctor&Fields

        private readonly IFileService _fileService;

        public SeriesEpisodesPagesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        #endregion

        /// <summary>
        /// Toplu resim ekleyen ve geriye dönen servis
        /// </summary>
        [HttpPost("MultiUploadImages")]
        [ProducesResponseType(typeof(SuccessDataResult<MultiUploadImagesCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> MultiUploadImagesAsync([FromForm] SeriesEpisodesMultiUploadImagesModel binding, CancellationToken cancellation)
        {
            var files_Result = await _fileService.UploadMultiSeriesImagesAsync(binding.Files, cancellation);

            var result = await Mediator.Send(new MultiUploadImagesCommand() { Files = files_Result }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Tekli resim ekleyen ve geriye dönen servis
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        [ProducesResponseType(typeof(SuccessDataResult<UploadImageCommandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> UploadImageAsync([FromForm] UploadImageModel binding, CancellationToken cancellation)
        {
            var files_Result = await _fileService.UploadSeriesImageAsync(binding.File, cancellation);

            var result = await Mediator.Send(new UploadImageCommand() { File = files_Result }, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Toplu seri(resimli) sayfaları ekleyen servis
        /// </summary>
        [HttpPost("CreateMultiSeriesPage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> CreateMultiSeriesPageAsync(CreateMultiSeriesPageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
            return CustomStandartReturnAction(result);
        }

        /// <summary>
        /// Toplu Resim Silen Servis
        /// </summary>
        [HttpPost("DeleteMultiImage")]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteMultiImageAsync(DeleteMultiImageCommand command, CancellationToken cancellation)
        {
            var result = await Mediator.Send(command, cancellation);
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
