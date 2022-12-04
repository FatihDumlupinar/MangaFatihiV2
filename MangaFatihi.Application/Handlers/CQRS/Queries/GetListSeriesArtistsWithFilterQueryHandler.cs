using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.CQRS.Bindings.Queries;
using MangaFatihi.Models.CQRS.DTOs.Queries;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
{
    public class GetListSeriesArtistsWithFilterQueryHandler : IQueryHandler<GetListSeriesArtistsWithFilterQuery, DataResult<GetListSeriesArtistsWithFilterQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public GetListSeriesArtistsWithFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<GetListSeriesArtistsWithFilterQueryDto>> Handle(GetListSeriesArtistsWithFilterQuery query, CancellationToken cancellationToken)
        {
            var iQuerayble = _unitOfWork.SeriesArtist.Find(i => i.IsActive);

            if (!string.IsNullOrEmpty(query.FullName))
            {
                iQuerayble = iQuerayble.Where(x => x.FullName.Contains(query.FullName, StringComparison.OrdinalIgnoreCase));
            }

            if (query.PageLength != 0)
            {
                if (query.PageNo <= 0)
                {
                    query.PageNo = 1;
                }

                int skip = query.PageNo * query.PageLength;

                iQuerayble = iQuerayble.Skip(skip).Take(query.PageLength);
            }

            var seriesArtists = await iQuerayble.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);

            var totalCount = await iQuerayble.CountAsync(cancellationToken);

            var returnModel = new GetListSeriesArtistsWithFilterQueryDto()
            {
                List = seriesArtists.Select(i => new SeriesArtistListModel()
                {
                    FullName = i.FullName,
                    Id = i.Id

                }).ToList(),

                TotalCount = totalCount

            };

            return new SuccessDataResult<GetListSeriesArtistsWithFilterQueryDto>(returnModel, ApplicationMessages.SuccessGetListProcess.GetMessage(), ApplicationMessages.SuccessGetListProcess);

        }

    }
}
