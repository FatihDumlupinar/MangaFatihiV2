using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.API.Commons;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
{
    public class GetSeriesInformationQueryHandler : IQueryHandler<GetSeriesInformationQuery, DataResult<GetSeriesInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSeriesInformationQueryHandler> _logger;
        private readonly UserManager<AppUser> _userManager;

        public GetSeriesInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSeriesInformationQueryHandler> logger, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        #endregion

        public async ValueTask<DataResult<GetSeriesInformationQueryDto>> Handle(GetSeriesInformationQuery request, CancellationToken cancellationToken)
        {
            var seriesId = Guid.Parse(request.SeriesId);

            var seriesEntity = await _unitOfWork.Series.Find(i => i.IsActive && i.Id == seriesId)
                .Include(i => i.SeriesAndSeriesArtists.Where(x => x.IsActive)).ThenInclude(i => i.SeriesArtist)
                .Include(i => i.SeriesAndSeriesCategories.Where(x => x.IsActive)).ThenInclude(i => i.SeriesCategory)
                .Include(i => i.SeriesAndSeriesAuthor.Where(x => x.IsActive)).ThenInclude(i => i.SeriesAuthor)
                .AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(cancellationToken);
            if (seriesEntity == default)
            {
                _logger.LogError(ApplicationMessages.ErrorSeriesNotFound.GetMessage(), ApplicationMessages.ErrorSeriesNotFound, seriesEntity);

                return new NotFoundDataResult<GetSeriesInformationQueryDto>(ApplicationMessages.ErrorSeriesNotFound.GetMessage(), ApplicationMessages.ErrorSeriesNotFound);
            }

            #region Seri ile ilişkilendirilmiş tüm kullanıcılar

            var userIds = new List<Guid>();

            if (seriesEntity.CreateUserId.HasValue)
            {
                userIds.Add(seriesEntity.CreateUserId.Value);
            }

            if (seriesEntity.UpdateUserId.HasValue)
            {
                userIds.Add(seriesEntity.UpdateUserId.Value);
            }

            var users = await _userManager.Users.Where(i => i.IsActive && userIds.Contains(i.Id)).ToListAsync(cancellationToken);

            #endregion

            var returnModel = new GetSeriesInformationQueryDto()
            {
                SeriesArtists = seriesEntity.SeriesAndSeriesArtists.Select(i => new StandartModel()
                {
                    Id = i.SeriesArtist.Id,
                    Name = i.SeriesArtist.FullName
                }).ToList(),

                SeriesAuthors = seriesEntity.SeriesAndSeriesAuthor.Select(i => new StandartModel()
                {
                    Id = i.SeriesAuthor.Id,
                    Name = i.SeriesAuthor.FullName
                }).ToList(),

                SeriesCategories = seriesEntity.SeriesAndSeriesCategories.Select(i => new StandartModel()
                {
                    Id = i.SeriesCategory.Id,
                    Name = i.SeriesCategory.Name
                }).ToList(),

                SeriesBroadcastStartDate = seriesEntity.BroadcastStartDate,
                SeriesIsNew = seriesEntity.IsNew,
                SeriesIsSlyder = seriesEntity.IsSlyder,
                SeriesIsUpToDate = seriesEntity.IsUpToDate,
                SeriesNote = seriesEntity.Note,
                SeriesProfileImgUrl = seriesEntity.ProfileImgUrl,
                SeriesStartDateOnPage = seriesEntity.StartDateOnPage,
                SeriesStatus = seriesEntity.StaticSeriesStatus.Name,
                SeriesStatusId = seriesEntity.StaticSeriesStatus.Id,
                SeriesStory = seriesEntity.Story,
                SeriesTitle = seriesEntity.Title,
                SeriesTitleAlternative = seriesEntity.TitleAlternative,
                SeriesTypes = seriesEntity.StaticSeriesTypes.Name,
                SeriesTypesId = seriesEntity.StaticSeriesTypes.Id,

                //todo SeriesEpisodes controller ı yazıldığında seriye göre bölümlerin listesini getirme servisi yazılacak

                CreatedUserName = users.Find(x => x.Id == seriesEntity.CreateUserId)?.FullName ?? "",
                UpdatedUserName = users.Find(x => x.Id == seriesEntity.UpdateUserId)?.FullName ?? ""
            };

            return new SuccessDataResult<GetSeriesInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
