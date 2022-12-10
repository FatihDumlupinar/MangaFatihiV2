using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesAuthorCommandHandler : ICommandHandler<DeleteSeriesAuthorCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSeriesAuthorCommandHandler> _logger;

        public DeleteSeriesAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSeriesAuthorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteSeriesAuthorCommand command, CancellationToken cancellationToken)
        {
            var seriesAuthorId = Guid.Parse(command.SeriesAuthorId);

            var seriesAuthorEntity = await _unitOfWork.SeriesAuthor.GetByIdAsync(seriesAuthorId, cancellationToken);
            if (seriesAuthorEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Yazarı"), ApplicationMessages.ErrorDefaultNotFound, seriesAuthorEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Yazarı"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesAuthor.DeleteAsync(seriesAuthorEntity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var seriesAndSeriesAuthorList_old = await _unitOfWork.SeriesAndSeriesAuthor
                .Find(i => i.IsActive && i.SeriesAuthorId == seriesAuthorId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesAuthorList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesAuthor.DeleteRangeAsync(seriesAndSeriesAuthorList_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }

    }
}
