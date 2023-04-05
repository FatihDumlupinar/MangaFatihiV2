using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteMultiPageCommandHandler : ICommandHandler<DeleteMultiPageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteMultiPageCommandHandler> _logger;

        public DeleteMultiPageCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteMultiPageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteMultiPageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisodePageEntities = await _unitOfWork.SeriesEpisodesPage
                .Find(i => i.IsActive && command.PageIds.Contains(i.Id))
                .ToListAsync(cancellationToken);
            if (seriesEpisodePageEntities == default || !seriesEpisodePageEntities.Any())
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfaları"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodePageEntities);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfaları"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesEpisodesPage.DeleteRangeAsync(seriesEpisodePageEntities, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }
    }
}
