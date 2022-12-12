using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteSeriesEpisodeCommandHandler : ICommandHandler<DeleteSeriesEpisodeCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSeriesEpisodeCommandHandler> _logger;

        public DeleteSeriesEpisodeCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSeriesEpisodeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteSeriesEpisodeCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisodeId = Guid.Parse(command.SeriesEpisodeId);

            var seriesEpisodeEntity = await _unitOfWork.SeriesEpisode.GetByIdAsync(seriesEpisodeId, cancellationToken);
            if (seriesEpisodeEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodeEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesEpisode.DeleteAsync(seriesEpisodeEntity, cancellationToken);

            var seriEpisodePageList_old = await _unitOfWork.SeriesEpisodesPage
                .Find(i => i.IsActive && i.SeriesEpisodesId == seriesEpisodeId)
                .ToListAsync(cancellationToken);
            if (seriEpisodePageList_old.Any())
            {
                await _unitOfWork.SeriesEpisodesPage.DeleteRangeAsync(seriEpisodePageList_old, cancellationToken);
            }

            //todo bölümün resimleri silinecek

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }

    }
}
