using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class EditSeriesCommandHandler : ICommandHandler<EditSeriesCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public EditSeriesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditSeriesCommand request, CancellationToken cancellationToken)
        {
            var seriesId = Guid.Parse(request.SeriesId);

            var seriesEntity = await _unitOfWork.Series.GetByIdAsync(seriesId);
            if (seriesEntity == default)
            {
                return new NotFoundDataResult<object>(ApplicationMessages.ErrorSeriesNotFound.GetMessage(), ApplicationMessages.ErrorSeriesNotFound);
            }

            seriesEntity.StaticSeriesStatusId = (int)request.SeriesStatusId;
            seriesEntity.StaticSeriesTypesId = (int)request.SeriesTypesId;

            seriesEntity.Story = request.Story;
            seriesEntity.BroadcastStartDate = request.BroadcastStartDate;
            seriesEntity.IsNew = request.IsNew;
            seriesEntity.IsSlyder = request.IsSlyder;
            seriesEntity.IsUpToDate = request.IsUpToDate;
            seriesEntity.Note = request.Note;
            seriesEntity.ProfileImgUrl = request.ProfileImgUrl;
            seriesEntity.StartDateOnPage = request.StartDateOnPage;
            seriesEntity.TitleAlternative = request.TitleAlternative;
            seriesEntity.Title = request.Title;

            #region Series And SeriesArtist

            var seriesAndSeriesArtistEntities_old = await _unitOfWork.SeriesAndSeriesArtist
                    .Find(i => i.IsActive && i.SeriesId == seriesId)
                    .ToListAsync(cancellationToken);
            if (seriesAndSeriesArtistEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesArtist.DeleteRangeAsync(seriesAndSeriesArtistEntities_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            if (request.SeriesArtistIds.Any())
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

            #endregion

            #region Series And SeriesAuthor

            var seriesAndSeriesAuthorsEntities_old = await _unitOfWork.SeriesAndSeriesAuthor
                    .Find(i => i.IsActive && i.SeriesId == seriesId)
                    .ToListAsync(cancellationToken);
            if (seriesAndSeriesAuthorsEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesAuthor.DeleteRangeAsync(seriesAndSeriesAuthorsEntities_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            if (request.SeriesAuthorIds.Any())
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

            #endregion

            #region Series And SeriesCategory

            var seriesAndSeriesCategoryEntities_old = await _unitOfWork.SeriesAndSeriesCategory
                   .Find(i => i.IsActive && i.SeriesId == seriesId)
                   .ToListAsync(cancellationToken);
            if (seriesAndSeriesCategoryEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesCategory.DeleteRangeAsync(seriesAndSeriesCategoryEntities_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            if (request.SeriesCategoryIds.Any())
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

            #endregion

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
