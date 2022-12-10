﻿using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
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
            var seriesArtistId = Guid.Parse(query.SeriesCategoryId);

            var seriesArtist = await _unitOfWork.SeriesCategory
                .Find(i => i.IsActive && i.Id == seriesArtistId)
                .Include(i => i.SeriesAndSeriesCategories.Where(x => x.IsActive && x.Series.IsActive)).ThenInclude(i => i.Series)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (seriesArtist == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound, seriesArtist);

                return new NotFoundDataResult<GetSeriesCategoryInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetSeriesCategoryInformationQueryDto()
            {
                Id = seriesArtistId,
                Name = seriesArtist.Name,
                SeriesList = seriesArtist.SeriesAndSeriesCategories.Select(i => new SmallSeriesListModel()
                {
                    SeriesId = i.Series.Id,
                    SeriesTitle = i.Series.Title
                }).ToList()

            };

            return new SuccessDataResult<GetSeriesCategoryInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}