using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class CreateSeriesCommandHandler : ICommandHandler<CreateSeriesCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public CreateSeriesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateSeriesCommand request, CancellationToken cancellationToken)
        {
            var seriesEntity = await _unitOfWork.Series.AddAsyncReturnEntity(new()
            {
                BroadcastStartDate = request.BroadcastStartDate,
                IsNew = request.IsNew,
                IsSlyder = request.IsSlyder,
                IsUpToDate = request.IsUpToDate,
                Note = request.Note,
                ProfileImgUrl = request.ProfileImgUrl,
                StartDateOnPage = request.StartDateOnPage,
                Story = request.Story,
                Title = request.Title,
                TitleAlternative = request.TitleAlternative,
                StaticSeriesStatusId = (int)request.SeriesStatusId,
                StaticSeriesTypesId = (int)request.SeriesTypesId,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            if (request.SeriesArtistIds != null && request.SeriesArtistIds.Any())
            {
                var seriesArtists = await _unitOfWork.SeriesArtist
                    .Find(i => i.IsActive && request.SeriesArtistIds.Contains(i.Id))
                    .Include(i => i.SeriesAndSeriesArtists)
                    .ToListAsync(cancellationToken);
                if (seriesArtists.Any())
                {
                    var seriesAndSeriesArtistAddListModel = seriesArtists.Select(i => new SeriesAndSeriesArtist()
                    {
                        Series = seriesEntity,
                        SeriesId = seriesEntity.Id,
                        SeriesArtist = i,
                        SeriesArtistId = i.Id,

                    }).ToList();

                    await _unitOfWork.SeriesAndSeriesArtist.AddRangeAsync(seriesAndSeriesArtistAddListModel, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }
            if (request.SeriesAuthorIds != null && request.SeriesAuthorIds.Any())
            {
                var seriesAuthors = await _unitOfWork.SeriesAuthor
                    .Find(i => i.IsActive && request.SeriesAuthorIds.Contains(i.Id))
                    .Include(i => i.SeriesAndSeriesAuthors)
                    .ToListAsync(cancellationToken);
                if (seriesAuthors.Any())
                {
                    var seriesAndSeriesAuthorAddListModel = seriesAuthors.Select(i => new SeriesAndSeriesAuthor()
                    {
                        Series = seriesEntity,
                        SeriesId = seriesEntity.Id,
                        SeriesAuthor = i,
                        SeriesAuthorId = i.Id,

                    }).ToList();

                    await _unitOfWork.SeriesAndSeriesAuthor.AddRangeAsync(seriesAndSeriesAuthorAddListModel, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }
            if (request.SeriesCategoryIds != null && request.SeriesCategoryIds.Any())
            {
                var seriesCategories = await _unitOfWork.SeriesCategory
                    .Find(i => i.IsActive && request.SeriesCategoryIds.Contains(i.Id))
                    .Include(i => i.SeriesAndSeriesCategories)
                    .ToListAsync(cancellationToken);
                if (seriesCategories.Any())
                {
                    var seriesAndSeriesCategoryAddListModel = seriesCategories.Select(i => new SeriesAndSeriesCategory()
                    {
                        Series = seriesEntity,
                        SeriesId = seriesEntity.Id,
                        SeriesCategory = i,
                        SeriesCategoryId = i.Id,

                    }).ToList();

                    await _unitOfWork.SeriesAndSeriesCategory.AddRangeAsync(seriesAndSeriesCategoryAddListModel, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}
