using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using Mediator;
using Microsoft.EntityFrameworkCore;
using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
{
    public class GetListSeriesEpisodesWithFilterQueryHandler : IQueryHandler<GetListSeriesEpisodesWithFilterQuery, DataResult<GetListSeriesEpisodesWithFilterQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public GetListSeriesEpisodesWithFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<GetListSeriesEpisodesWithFilterQueryDto>> Handle(GetListSeriesEpisodesWithFilterQuery query, CancellationToken cancellationToken)
        {
            var iQuerayble = _unitOfWork.SeriesEpisode.Find(i => i.IsActive);

            #region Filters

            if (!string.IsNullOrEmpty(query.Search))
            {
                iQuerayble = iQuerayble.Where(x => x.Title != null && x.Title.Contains(query.Search));
            }

            #endregion

            #region Sıralama

            query.OrderBy = query.OrderBy.ToLower().Trim();

            switch (query.OrderBy)
            {
                case "id":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.Id) : iQuerayble.OrderBy(i => i.Id);
                    break;

                case "title":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.Title) : iQuerayble.OrderBy(i => i.Title);
                    break;

                case "episodeno":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.EpisodeNo) : iQuerayble.OrderBy(i => i.EpisodeNo);
                    break;

                case "seriesepisodetypesid":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.StaticSeriesEpisodeTypeId) : iQuerayble.OrderBy(i => i.StaticSeriesEpisodeTypeId);
                    break;

                case "seriesepisodetype":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.StaticSeriesEpisodeType.Name) : iQuerayble.OrderBy(i => i.StaticSeriesEpisodeType.Name);
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

            var seriesEpisodes = await iQuerayble
                .Include(i => i.EditorUser)
                .Include(i => i.TranslatorUser)
                .Include(i => i.StaticSeriesEpisodeType)
                .AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);

            #region Listenin diğer elemanları

            var onlyUserIds = new List<Guid>();
            foreach (var seriesArtist in seriesEpisodes)
            {
                if (seriesArtist.CreateUserId.HasValue)
                {
                    onlyUserIds.Add(seriesArtist.CreateUserId.Value);
                }

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

            var returnModel = new GetListSeriesEpisodesWithFilterQueryDto()
            {
                List = seriesEpisodes.Select(i => new SeriesEpisodeListModel()
                {
                    Id = i.Id,

                    Title = i.Title,
                    EpisodeNo = i.EpisodeNo,
                    SeriesEpisodeTypesId = i.StaticSeriesEpisodeTypeId,
                    SeriesEpisodeType = i.StaticSeriesEpisodeType.Name,

                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate,

                    EditorUser = i.EditorUser?.FullName,
                    TranslatorUser = i.TranslatorUser?.FullName,

                    CreateUser = users.FirstOrDefault(x => x.Id == i.CreateUserId)?.FullName,
                    UpdateUser = users.FirstOrDefault(x => x.Id == i.UpdateUserId)?.FullName

                }).ToList(),

                TotalCount = totalCount

            };

            return new SuccessDataResult<GetListSeriesEpisodesWithFilterQueryDto>(returnModel, ApplicationMessages.SuccessGetListProcess.GetMessage(), ApplicationMessages.SuccessGetListProcess);
        }

    }

}
