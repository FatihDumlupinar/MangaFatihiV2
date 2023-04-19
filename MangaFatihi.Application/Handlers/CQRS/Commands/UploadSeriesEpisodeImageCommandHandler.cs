using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Infrastructure.Services.SeriesEpisode;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class UploadSeriesEpisodeImageCommandHandler : ICommandHandler<UploadSeriesEpisodeImageCommand, DataResult<UploadSeriesEpisodeImageCommandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UploadSeriesEpisodeImageCommandHandler> _logger;
        private readonly ISeriesEpisodeFileService _seriesEpisodeFileService;

        public UploadSeriesEpisodeImageCommandHandler(IUnitOfWork unitOfWork, ILogger<UploadSeriesEpisodeImageCommandHandler> logger, ISeriesEpisodeFileService seriesEpisodeFileService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _seriesEpisodeFileService = seriesEpisodeFileService;
        }

        public async ValueTask<DataResult<UploadSeriesEpisodeImageCommandDto>> Handle(UploadSeriesEpisodeImageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode
                .Find(e => e.Id == command.SeriesEpisodeId)
                .Include(x => x.Series)
                .FirstOrDefaultAsync(i => i.IsActive && i.Series.IsActive, cancellationToken);
            if (seriesEpisode is null)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<UploadSeriesEpisodeImageCommandDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var uploadSeriesEpisodeImageUrl = await _seriesEpisodeFileService.UploadSeriesEpisodeImageAsync(command.File, seriesEpisode.Series.Title, seriesEpisode.EpisodeNo, cancellationToken);

            var seriesEpisodePageEntity = await _unitOfWork.SeriesEpisodesPage.AddAsyncReturnEntity(new()
            {
                PageImageUrl = uploadSeriesEpisodeImageUrl,
                PageNo = command.PageNo,
                SeriesEpisodesId = seriesEpisode.Id,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            var returnModel = new UploadSeriesEpisodeImageCommandDto()
            {
                Model = new()
                {
                    PageImageId = seriesEpisodePageEntity.Id,
                    PageImageUrl = uploadSeriesEpisodeImageUrl,
                    PageNo = command.PageNo,
                }
            };

            return new SuccessDataResult<UploadSeriesEpisodeImageCommandDto>(returnModel, ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}
