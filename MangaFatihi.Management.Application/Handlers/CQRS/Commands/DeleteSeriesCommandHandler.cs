using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesCommandHandler : ICommandHandler<DeleteSeriesCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public DeleteSeriesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteSeriesCommand request, CancellationToken cancellationToken)
        {
            var seriesId = Guid.Parse(request.SeriesId);

            var seriesEntity = await _unitOfWork.Series.GetByIdAsync(seriesId, cancellationToken);
            if (seriesEntity == default)
            {
                return new NotFoundDataResult<object>(ApplicationMessages.ErrorSeriesNotFound.GetMessage(), ApplicationMessages.ErrorSeriesNotFound);
            }

            await _unitOfWork.Series.DeleteAsync(seriesEntity, cancellationToken);

            var seriesAndSeriesArtistEntities_old = await _unitOfWork.SeriesAndSeriesArtist
                    .Find(i => i.IsActive && i.SeriesId == seriesId)
                    .ToListAsync(cancellationToken);
            if (seriesAndSeriesArtistEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesArtist.DeleteRangeAsync(seriesAndSeriesArtistEntities_old, cancellationToken);
            }

            var seriesAndSeriesAuthorsEntities_old = await _unitOfWork.SeriesAndSeriesAuthor
                    .Find(i => i.IsActive && i.SeriesId == seriesId)
                    .ToListAsync(cancellationToken);
            if (seriesAndSeriesAuthorsEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesAuthor.DeleteRangeAsync(seriesAndSeriesAuthorsEntities_old, cancellationToken);
            }

            var seriesAndSeriesCategoryEntities_old = await _unitOfWork.SeriesAndSeriesCategory
                   .Find(i => i.IsActive && i.SeriesId == seriesId)
                   .ToListAsync(cancellationToken);
            if (seriesAndSeriesCategoryEntities_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesCategory.DeleteRangeAsync(seriesAndSeriesCategoryEntities_old, cancellationToken);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }
    }
}
