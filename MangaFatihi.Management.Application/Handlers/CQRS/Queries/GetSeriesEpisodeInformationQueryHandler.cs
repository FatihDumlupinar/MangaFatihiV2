using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
{
    public class GetSeriesEpisodeInformationQueryHandler : IQueryHandler<GetSeriesEpisodeInformationQuery, DataResult<GetSeriesEpisodeInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSeriesEpisodeInformationQueryHandler> _logger;

        public GetSeriesEpisodeInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSeriesEpisodeInformationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<GetSeriesEpisodeInformationQueryDto>> Handle(GetSeriesEpisodeInformationQuery query, CancellationToken cancellationToken)
        {
            var seriesEpisodeId = Guid.Parse(query.SeriesEpisodeId);

            var seriesEpisode = await _unitOfWork.SeriesEpisode
                .Find(i => i.IsActive && i.Id == seriesEpisodeId)
                .Include(i => i.Series)
                .Include(i => i.Team)
                .Include(i => i.EditorUser)
                .Include(i => i.TranslatorUser)
                .Include(i => i.StaticSeriesEpisodeType)
                .Include(i => i.SeriesEpisodesPages.Where(x => x.IsActive))
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (seriesEpisode == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<GetSeriesEpisodeInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetSeriesEpisodeInformationQueryDto()
            {
                Id = seriesEpisode.Id,
                EpisodeNo = seriesEpisode.EpisodeNo,
                FileSizeMb = seriesEpisode.FileSizeMb,
                IsOnAir = seriesEpisode.IsOnAir,
                Note = seriesEpisode.Note,
                ViewsCount = seriesEpisode.ViewsCount,
                Title = seriesEpisode.Title,

                SeriesId = seriesEpisode.SeriesId,
                Series = seriesEpisode.Series.Title,

                Team = seriesEpisode.Team?.Name,
                EditorUser = seriesEpisode.EditorUser?.FullName,
                TranslatorUser = seriesEpisode.TranslatorUser?.FullName,

                SeriesEpisodeType = seriesEpisode.StaticSeriesEpisodeType.Name,
                SeriesEpisodeTypeId = seriesEpisode.StaticSeriesEpisodeTypeId,

                SeriesEpisodesPageList = seriesEpisode.SeriesEpisodesPages.Select(i => new SeriesEpisodesDetailPageListModel()
                {
                    PageContent = i.PageContent,
                    PageImageUrl = i.PageImageUrl,
                    PageNo = i.PageNo,

                }).ToList(),

            };

            return new SuccessDataResult<GetSeriesEpisodeInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
