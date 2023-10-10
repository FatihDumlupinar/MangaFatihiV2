using MangaFatihi.Management.WebAPI.Controllers.Base;
using MangaFatihi.Shared.Authorize.Policies;
using MangaFatihi.Shared.Models.API.Commons;
using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Commands;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.DataResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.Management.WebAPI.Controllers
{
    /// <summary>
    /// Seri bölümleri sayfaların CRUD işlemleri
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.List)]
    [ApiController]
    public class SeriesEpisodesPagesController : CustomBaseController<SeriesEpisodesPagesController>
    {
        /// <summary>
        /// Toplu resim ekleyen ve geriye dönen servis
        /// </summary>
        [HttpPost("{seriesEpisodeId}/MultiUploadImages")]
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Create)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Create)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Create)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Create)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Update)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Update)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Delete)]
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
        [Authorize(Policy = PolicyTypes.Claim_SeriesEpisodesPage.Delete)]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<>), StatusCodes.Status400BadRequest)]
        public async ValueTask<IActionResult> DeleteOnePageAsync(string seriesPageId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(new DeleteOnePageCommand() { SeriesPageId = seriesPageId }, cancellation);
            return CustomStandartReturnAction(result);
        }

    }
}
