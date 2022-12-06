using MangaFatihi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dapper;
using MangaFatihi.Domain.Constants;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Commonns;
using Mediator;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using MangaFatihi.Models.Bindings.CQRS.Queries;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
{
    public class GetListSeriesWithFilterQueryHandler : IQueryHandler<GetListSeriesWithFilterQuery, DataResult<GetListSeriesWithFilterQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public GetListSeriesWithFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<GetListSeriesWithFilterQueryDto>> Handle(GetListSeriesWithFilterQuery request, CancellationToken cancellationToken)
        {
            await using var _dbConnection = _unitOfWork.DbContext.Database.GetDbConnection();

            var dynoParams = new DynamicParameters();
            var sqlQuery = @"
                  SELECT
                    s.Id AS SeriesId,
                    s.Title AS SeriesTitle,
                    s.ProfileImgUrl AS SeriesProfileImgUrl,
                    s.BroadcastStartDate AS SeriesBroadcastStartDate,
                    s.StartDateOnPage AS SeriesStartDateOnPage,
                    ssest.Name AS SeriesStatus,
                    ssest.Id AS SeriesStatusId,
                    ssety.Name AS SeriesType,
                    ssety.Id AS SeriesTypesId,
                    (SELECT anu.FullName FROM AspNetUsers anu WHERE anu.Id=s.CreateUserId AND anu.IsActive=1) AS CreatedUserName,
                    (SELECT anu.FullName FROM AspNetUsers anu WHERE anu.Id=s.UpdateUserId AND anu.IsActive=1) AS UpdatedUserName
                  FROM
                    Series s
                    LEFT JOIN StaticSeriesStatus ssest ON ssest.Id=s.StaticSeriesStatusId 
                    LEFT JOIN StaticSeriesTypes ssety ON ssety.Id=s.StaticSeriesTypesId
                  WHERE 
                    s.IsActive = 1 ";

            #region Filters

            if (request.SeriesArtistIds != null && request.SeriesArtistIds.Any())
            {
                sqlQuery += @" AND s.Id IN (SELECT sasa.SeriesId FROM SeriesAndSeriesArtists sasa, SeriesArtists sa WHERE sasa.IsActive=1 AND sa.IsActive=1 AND sasa.SeriesArtistId=sa.Id AND sa.Id IN @SeriesArtistIds)";
                dynoParams.Add("SeriesArtistIds", request.SeriesArtistIds);
            }
            if (request.SeriesAuthorIds != null && request.SeriesAuthorIds.Any())
            {
                sqlQuery += @" AND s.Id IN (SELECT sasa.SeriesId FROM SeriesAndSeriesAuthors sasa, SeriesAuthors sa WHERE sasa.IsActive=1 AND sa.IsActive=1 AND sasa.SeriesAuthorId=sa.Id AND sa.Id IN @SeriesAuthorIds)";
                dynoParams.Add("SeriesAuthorIds", request.SeriesAuthorIds);
            }
            if (request.SeriesCategoryIds != null && request.SeriesCategoryIds.Any())
            {
                sqlQuery += @" AND s.Id IN (SELECT sasc.SeriesId FROM SeriesAndSeriesCategories sasc, SeriesCategories sc WHERE sasc.IsActive=1 AND sc.IsActive=1 AND sasc.SeriesCategoryId=sc.Id AND sc.Id IN @SeriesCategoryIds)";
                dynoParams.Add("SeriesCategoryIds", request.SeriesCategoryIds);
            }
            if (request.SeriesStatusIds != null && request.SeriesStatusIds.Any())
            {
                sqlQuery += @" AND ssest.Id IN @SeriesStatusIds ";
                dynoParams.Add("SeriesStatusIds", request.SeriesStatusIds);
            }
            if (request.SeriesTypeIds != null && request.SeriesTypeIds.Any())
            {
                sqlQuery += @" AND ssety.Id IN @SeriesTypeIds ";
                dynoParams.Add("SeriesTypeIds", request.SeriesTypeIds);
            }


            #endregion

            #region TotalCount

            string totalCountSqlQuery = $"SELECT COUNT(*) FROM ({sqlQuery}) as t";
            DynamicParameters totalCountdynoParams = dynoParams;

            var totalCount = await _dbConnection.QueryFirstOrDefaultAsync<int>(totalCountSqlQuery, totalCountdynoParams);

            #endregion

            #region Sıralama

            var sortBy = "desc";

            if (!request.OrderByDesc)
            {
                sortBy = "asc";
            }

            request.OrderBy = request.OrderBy.ToLower().Trim();

            switch (request.OrderBy)
            {
                case "seriesid":
                    sqlQuery += $" ORDER BY s.Id {sortBy} ";
                    break;

                case "seriestitle":
                    sqlQuery += $" ORDER BY s.Title {sortBy} ";
                    break;

                case "seriesprofileimgurl":
                    sqlQuery += $" ORDER BY s.ProfileImgUrl {sortBy} ";
                    break;

                case "seriesbroadcaststartdate":
                    sqlQuery += $" ORDER BY s.BroadcastStartDate {sortBy} ";
                    break;

                case "seriesstartdateonpage":
                    sqlQuery += $" ORDER BY s.StartDateOnPage {sortBy} ";
                    break;

                case "seriesstatus":
                    sqlQuery += $" ORDER BY ssest.Name {sortBy} ";
                    break;

                case "seriestype":
                    sqlQuery += $" ORDER BY ssety.Name {sortBy} ";
                    break;

                default:
                    sqlQuery += $" ORDER BY s.Id {sortBy} ";
                    break;
            }

            #endregion

            #region Sayfalama

            if (request.PageLength != 0)//eğer sıfır olursa bütün verileri sıfır değilde gönderilen length kadar
            {
                if (request.PageNo <= 0)
                {
                    request.PageNo = 1;
                }

                sqlQuery += $" OFFSET @skip ROWS ";//sayfalama içindir. Örneğin " Offset 10 rows " ise 10 satır atla sonrakileri al demek 
                int skip = (request.PageNo - 1) * request.PageLength;
                dynoParams.Add("skip", skip);

                sqlQuery += $" FETCH NEXT @take ROWS ONLY ";//ne kadar verinin getirileceği
                dynoParams.Add("take", request.PageLength);

            }

            #endregion

            var seriesList = await _dbConnection.QueryAsync<SeriesListModel>(sqlQuery, dynoParams);

            #region Listenin diğer elemanları

            var onlySeriesIds = seriesList.Select(x => x.SeriesId).ToList();

            if (onlySeriesIds.Any())
            {
                var seriesArtists = await _unitOfWork.SeriesArtist
                    .Find(i => i.IsActive && i.SeriesAndSeriesArtists.Any(x => onlySeriesIds.Contains(x.SeriesId)))
                    .Include(i => i.SeriesAndSeriesArtists)
                    .ToListAsync(cancellationToken);

                var seriesAuthors = await _unitOfWork.SeriesAuthor
                     .Find(i => i.IsActive && i.SeriesAndSeriesAuthors.Any(x => onlySeriesIds.Contains(x.SeriesId)))
                    .Include(i => i.SeriesAndSeriesAuthors)
                    .ToListAsync(cancellationToken);

                var seriesCategories = await _unitOfWork.SeriesCategory
                     .Find(i => i.IsActive && i.SeriesAndSeriesCategories.Any(x => onlySeriesIds.Contains(x.SeriesId)))
                    .Include(i => i.SeriesAndSeriesCategories)
                    .ToListAsync(cancellationToken);

                foreach (var series in seriesList)
                {
                    series.SeriesArtists = seriesArtists
                        .Where(i => i.SeriesAndSeriesArtists
                        .Any(x => x.SeriesId == series.SeriesId))
                        .Select(i => new StandartModel()
                        {
                            Id = i.Id,
                            Name = i.FullName
                        })
                        .ToList();

                    series.SeriesAuthors = seriesAuthors
                        .Where(i => i.SeriesAndSeriesAuthors
                        .Any(x => x.SeriesId == series.SeriesId))
                        .Select(i => new StandartModel()
                        {
                            Id = i.Id,
                            Name = i.FullName
                        })
                        .ToList();

                    series.SeriesCategories = seriesCategories
                        .Where(i => i.SeriesAndSeriesCategories
                        .Any(x => x.SeriesId == series.SeriesId))
                        .Select(i => new StandartModel()
                        {
                            Id = i.Id,
                            Name = i.Name
                        })
                        .ToList();

                }

            }

            #endregion

            var returnModel = new GetListSeriesWithFilterQueryDto()
            {
                List = seriesList.ToList(),
                TotalCount = totalCount
            };

            return new SuccessDataResult<GetListSeriesWithFilterQueryDto>(returnModel, ApplicationMessages.SuccessGetListProcess.GetMessage(), ApplicationMessages.SuccessGetListProcess);
        }
    }
}
