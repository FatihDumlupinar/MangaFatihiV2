using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class DeleteOnePageCommandHandler : ICommandHandler<DeleteOnePageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteOnePageCommandHandler> _logger;

        public DeleteOnePageCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteOnePageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteOnePageCommand command, CancellationToken cancellationToken)
        {
            var seriesPageId = Guid.Parse(command.SeriesPageId);

            var seriesEpisodePageEntity = await _unitOfWork.SeriesEpisodesPage.GetByIdAsync(seriesPageId, cancellationToken);
            if (seriesEpisodePageEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfası"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodePageEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfası"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesEpisodesPage.DeleteAsync(seriesEpisodePageEntity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }
    }
}
