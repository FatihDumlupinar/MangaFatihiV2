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
    public class GetSeriesAuthorInformationQueryHandler : IQueryHandler<GetSeriesAuthorInformationQuery, DataResult<GetSeriesAuthorInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSeriesAuthorInformationQueryHandler> _logger;

        public GetSeriesAuthorInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSeriesAuthorInformationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<GetSeriesAuthorInformationQueryDto>> Handle(GetSeriesAuthorInformationQuery query, CancellationToken cancellationToken)
        {
            var seriesArtistId = Guid.Parse(query.SeriesAuthorId);

            var seriesArtist = await _unitOfWork.SeriesAuthor
                .Find(i => i.IsActive && i.Id == seriesArtistId)
                .Include(i => i.SeriesAndSeriesAuthors.Where(x => x.IsActive && x.Series.IsActive)).ThenInclude(i => i.Series)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (seriesArtist == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound, seriesArtist);

                return new NotFoundDataResult<GetSeriesAuthorInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Yazarı"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetSeriesAuthorInformationQueryDto()
            {
                Id = seriesArtistId,
                FullName = seriesArtist.FullName,
                SeriesList = seriesArtist.SeriesAndSeriesAuthors.Select(i => new SmallSeriesListModel()
                {
                    SeriesId = i.Series.Id,
                    SeriesTitle = i.Series.Title
                }).ToList()

            };

            return new SuccessDataResult<GetSeriesAuthorInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
