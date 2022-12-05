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

            #region Filters

            if (!string.IsNullOrEmpty(query.Search))
            {
                iQuerayble = iQuerayble.Where(x => x.FullName.Contains(query.Search));
            }

            #endregion

            #region Sıralama

            query.OrderBy = query.OrderBy.ToLower().Trim();

            switch (query.OrderBy)
            {
                case "id":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.Id) : iQuerayble.OrderBy(i => i.Id);
                    break;

                case "fullname":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.FullName) : iQuerayble.OrderBy(i => i.FullName);
                    break;

                case "createdate":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.CreateDate) : iQuerayble.OrderBy(i => i.CreateDate);
                    break;

                case "updatedate":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.UpdateDate) : iQuerayble.OrderBy(i => i.UpdateDate);
                    break;

                default:
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.CreateDate) : iQuerayble.OrderBy(i => i.CreateDate);
                    break;
            }

            #endregion

            var totalCount = await iQuerayble.CountAsync(cancellationToken);

            #region Sayfalama

            if (query.PageLength != 0)
            {
                if (query.PageNo <= 0)
                {
                    query.PageNo = 1;
                }

                var skip = (query.PageNo - 1) * query.PageLength;

                iQuerayble = iQuerayble.Skip(skip).Take(query.PageLength);
            }

            #endregion

            var seriesArtists = await iQuerayble.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);

            #region Listenin diğer elemanları

            var onlyUserIds = new List<Guid>();
            foreach (var seriesArtist in seriesArtists)
            {
                onlyUserIds.Add(seriesArtist.CreateUserId);
                if (seriesArtist.UpdateUserId.HasValue)
                {
                    onlyUserIds.Add(seriesArtist.UpdateUserId.Value);
                }
            }

            var users = await _unitOfWork.UserManager.Users
                .Where(i => i.IsActive && onlyUserIds.Contains(i.Id))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync(cancellationToken);

            #endregion

            var returnModel = new GetListSeriesArtistsWithFilterQueryDto()
            {
                List = seriesArtists.Select(i => new SeriesArtistListModel()
                {
                    FullName = i.FullName,
                    Id = i.Id,
                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate,
                    CreateUser = users.FirstOrDefault(x => x.Id == i.CreateUserId)?.FullName,
                    UpdateUser = users.FirstOrDefault(x => x.Id == i.UpdateUserId)?.FullName

                }).ToList(),

                TotalCount = totalCount

            };

            return new SuccessDataResult<GetListSeriesArtistsWithFilterQueryDto>(returnModel, ApplicationMessages.SuccessGetListProcess.GetMessage(), ApplicationMessages.SuccessGetListProcess);
        }

    }
}
