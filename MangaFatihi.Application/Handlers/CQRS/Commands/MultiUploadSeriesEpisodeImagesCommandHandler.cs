using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Infrastructure.Services.SeriesEpisode;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class MultiUploadSeriesEpisodeImagesCommandHandler : ICommandHandler<MultiUploadSeriesEpisodeImagesCommand, DataResult<MultiUploadSeriesEpisodeImagesCommandDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MultiUploadSeriesEpisodeImagesCommandHandler> _logger;
        private readonly ISeriesEpisodeFileService _seriesEpisodeFileService;

        public MultiUploadSeriesEpisodeImagesCommandHandler(IUnitOfWork unitOfWork, ILogger<MultiUploadSeriesEpisodeImagesCommandHandler> logger, ISeriesEpisodeFileService seriesEpisodeFileService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _seriesEpisodeFileService = seriesEpisodeFileService;
        }

        public async ValueTask<DataResult<MultiUploadSeriesEpisodeImagesCommandDto>> Handle(MultiUploadSeriesEpisodeImagesCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode
                .Find(e => e.Id == command.SeriesEpisodeId)
                .Include(x => x.Series)
                .FirstOrDefaultAsync(i => i.IsActive && i.Series.IsActive, cancellationToken);
            if (seriesEpisode is null)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<MultiUploadSeriesEpisodeImagesCommandDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var uploadSeriesEpisodeImageUrl = await _seriesEpisodeFileService.UploadMultiSeriesEpisodeIImagesAsync(command.Files, seriesEpisode.Series.Title, seriesEpisode.EpisodeNo, cancellationToken);

            var seriesEpisodePageAddListModel = new List<SeriesEpisodesPage>(capacity: uploadSeriesEpisodeImageUrl.Count);
            for (int i = 0; i < uploadSeriesEpisodeImageUrl.Count; i++)
            {
                seriesEpisodePageAddListModel.Add(new()
                {
                    PageImageUrl = uploadSeriesEpisodeImageUrl[i],
                    PageNo = i,
                    SeriesEpisodesId = command.SeriesEpisodeId

                });
            }

            var seriesEpisodePageEntityList = new List<SeriesEpisodesPage>(capacity: uploadSeriesEpisodeImageUrl.Count);
            foreach (var seriesEpisodesPage in seriesEpisodePageAddListModel)
            {
                var entity = await _unitOfWork.SeriesEpisodesPage.AddAsyncReturnEntity(seriesEpisodesPage, cancellationToken);
                seriesEpisodePageEntityList.Add(entity);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            var returnModel = new MultiUploadSeriesEpisodeImagesCommandDto()
            {
                List = seriesEpisodePageEntityList.Select(i => new UploadSeriesEpisodeModel()
                {
                    PageImageId = i.Id,
                    PageImageUrl = i.PageImageUrl ?? "",
                    PageNo = i.PageNo
                }).ToList(),
            };

            return new SuccessDataResult<MultiUploadSeriesEpisodeImagesCommandDto>(returnModel, ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }
    }
}
