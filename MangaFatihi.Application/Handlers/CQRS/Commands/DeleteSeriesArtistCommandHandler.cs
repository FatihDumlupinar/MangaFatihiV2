using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesArtistCommandHandler : ICommandHandler<DeleteSeriesArtistCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSeriesArtistCommandHandler> _logger;

        public DeleteSeriesArtistCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSeriesArtistCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteSeriesArtistCommand command, CancellationToken cancellationToken)
        {
            var seriesArtistId = Guid.Parse(command.SeriesArtistId);

            var seriesArtistEntity = await _unitOfWork.SeriesArtist.GetByIdAsync(seriesArtistId, cancellationToken);
            if (seriesArtistEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound, seriesArtistEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorSeriesNotFound);
            }

            await _unitOfWork.SeriesArtist.DeleteAsync(seriesArtistEntity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            var seriesAndSeriesArtistList_old = await _unitOfWork.SeriesAndSeriesArtist
                .Find(i => i.IsActive && i.SeriesArtistId == seriesArtistId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesArtistList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesArtist.DeleteRangeAsync(seriesAndSeriesArtistList_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }

    }
}
