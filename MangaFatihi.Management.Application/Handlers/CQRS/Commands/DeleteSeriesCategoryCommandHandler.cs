using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesCategoryCommandHandler : ICommandHandler<DeleteSeriesCategoryCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSeriesCategoryCommandHandler> _logger;

        public DeleteSeriesCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSeriesCategoryCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteSeriesCategoryCommand command, CancellationToken cancellationToken)
        {
            var seriesCategoryId = Guid.Parse(command.SeriesCategoryId);

            var seriesCategoryEntity = await _unitOfWork.SeriesCategory.GetByIdAsync(seriesCategoryId, cancellationToken);
            if (seriesCategoryEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound, seriesCategoryEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Kategorisi"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesCategory.DeleteAsync(seriesCategoryEntity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var seriesAndSeriesCategoryList_old = await _unitOfWork.SeriesAndSeriesCategory
                .Find(i => i.IsActive && i.SeriesCategoryId == seriesCategoryId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesCategoryList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesCategory.DeleteRangeAsync(seriesAndSeriesCategoryList_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }

    }
}
