using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.API.Commons.Series;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
{
    public class GetSeriesArtistInformationQueryHandler : IQueryHandler<GetSeriesArtistInformationQuery, DataResult<GetSeriesArtistInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSeriesArtistInformationQueryHandler> _logger;

        public GetSeriesArtistInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSeriesArtistInformationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<GetSeriesArtistInformationQueryDto>> Handle(GetSeriesArtistInformationQuery query, CancellationToken cancellationToken)
        {
            var seriesArtistId = Guid.Parse(query.SeriesArtistId);

            var seriesArtist = await _unitOfWork.SeriesArtist
                .Find(i => i.IsActive && i.Id == seriesArtistId)
                .Include(i => i.SeriesAndSeriesArtists.Where(x => x.IsActive && x.Series.IsActive)).ThenInclude(i => i.Series)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (seriesArtist == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound, seriesArtist);

                return new NotFoundDataResult<GetSeriesArtistInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetSeriesArtistInformationQueryDto()
            {
                Id = seriesArtistId,
                FullName = seriesArtist.FullName,
                SeriesList = seriesArtist.SeriesAndSeriesArtists.Select(i => new SmallSeriesListModel()
                {
                    SeriesId = i.Series.Id,
                    SeriesTitle = i.Series.Title
                }).ToList()

            };

            return new SuccessDataResult<GetSeriesArtistInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
