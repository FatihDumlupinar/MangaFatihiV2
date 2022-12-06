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
    public class EditSeriesAuthorCommandHandler : ICommandHandler<EditSeriesAuthorCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditSeriesAuthorCommandHandler> _logger;

        public EditSeriesAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<EditSeriesAuthorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditSeriesAuthorCommand command, CancellationToken cancellationToken)
        {
            var seriesAuthorId = Guid.Parse(command.Id);

            var seriesAuthorEntity = await _unitOfWork.SeriesAuthor.GetByIdAsync(seriesAuthorId, cancellationToken);
            if (seriesAuthorEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Yazarı"), ApplicationMessages.ErrorDefaultNotFound, seriesAuthorEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Yazarı"), ApplicationMessages.ErrorSeriesNotFound);
            }

            seriesAuthorEntity.FullName = command.FullName;

            await _unitOfWork.SeriesAuthor.UpdateAsync(seriesAuthorEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            //Eski olanları sil
            var seriesAndSeriesAuthorList_old = await _unitOfWork.SeriesAndSeriesAuthor
                .Find(i => i.IsActive && i.SeriesAuthorId == seriesAuthorId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesAuthorList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesAuthor.DeleteRangeAsync(seriesAndSeriesAuthorList_old, cancellationToken);
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
                    var seriesAndSeriesAuthorsList = series.Select(i => new SeriesAndSeriesAuthor()
                    {
                        SeriesId = i.Id,
                        SeriesAuthorId = seriesAuthorEntity.Id
                    });

                    //Yenileri ekle
                    await _unitOfWork.SeriesAndSeriesAuthor.AddRangeAsync(seriesAndSeriesAuthorsList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }

            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }

    }
}
