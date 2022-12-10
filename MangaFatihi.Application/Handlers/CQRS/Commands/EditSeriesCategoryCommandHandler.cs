using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class EditSeriesCategoryCommandHandler : ICommandHandler<EditSeriesCategoryCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditSeriesCategoryCommandHandler> _logger;

        public EditSeriesCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<EditSeriesCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditSeriesCategoryCommand command, CancellationToken cancellationToken)
        {
            var seriesCategoryId = Guid.Parse(command.Id);

            var seriesCategoryEntity = await _unitOfWork.SeriesCategory.GetByIdAsync(seriesCategoryId, cancellationToken);
            if (seriesCategoryEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound, seriesCategoryEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound);
            }

            seriesCategoryEntity.Name = command.Name;

            await _unitOfWork.SeriesCategory.UpdateAsync(seriesCategoryEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            //Eski olanları sil
            var seriesAndSeriesCategoryList_old = await _unitOfWork.SeriesAndSeriesCategory
                .Find(i => i.IsActive && i.SeriesCategoryId == seriesCategoryId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesCategoryList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesCategory.DeleteRangeAsync(seriesAndSeriesCategoryList_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            if (command.SeriesIds != null && command.SeriesIds.Any())
            {
                var series = await _unitOfWork.Series
                    .Find(i => i.IsActive && command.SeriesIds.Contains(i.Id))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync(cancellationToken);
                if (series.Any())
                {
                    var seriesAndSeriesCategorysList = series.Select(i => new SeriesAndSeriesCategory()
                    {
                        SeriesId = i.Id,
                        SeriesCategoryId = seriesCategoryEntity.Id
                    });

                    //Yenileri ekle
                    await _unitOfWork.SeriesAndSeriesCategory.AddRangeAsync(seriesAndSeriesCategorysList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }

            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
