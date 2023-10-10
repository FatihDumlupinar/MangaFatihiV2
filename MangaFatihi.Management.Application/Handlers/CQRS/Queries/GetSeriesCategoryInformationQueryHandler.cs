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
    public class GetSeriesCategoryInformationQueryHandler : IQueryHandler<GetSeriesCategoryInformationQuery, DataResult<GetSeriesCategoryInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSeriesCategoryInformationQueryHandler> _logger;

        public GetSeriesCategoryInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSeriesCategoryInformationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<GetSeriesCategoryInformationQueryDto>> Handle(GetSeriesCategoryInformationQuery query, CancellationToken cancellationToken)
        {
            var seriesCategoryId = Guid.Parse(query.SeriesCategoryId);

            var seriesCategory = await _unitOfWork.SeriesCategory
                .Find(i => i.IsActive && i.Id == seriesCategoryId)
                .Include(i => i.SeriesAndSeriesCategories.Where(x => x.IsActive && x.Series.IsActive)).ThenInclude(i => i.Series)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (seriesCategory == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound, seriesCategory);

                return new NotFoundDataResult<GetSeriesCategoryInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetSeriesCategoryInformationQueryDto()
            {
                Id = seriesCategoryId,
                Name = seriesCategory.Name,
                SeriesList = seriesCategory.SeriesAndSeriesCategories.Select(i => new SmallSeriesListModel()
                {
                    SeriesId = i.Series.Id,
                    SeriesTitle = i.Series.Title
                }).ToList()

            };

            return new SuccessDataResult<GetSeriesCategoryInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
