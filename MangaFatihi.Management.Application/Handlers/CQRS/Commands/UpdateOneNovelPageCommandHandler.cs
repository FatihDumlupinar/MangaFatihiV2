using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Constants;
using Mediator;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class UpdateOneNovelPageCommandHandler : ICommandHandler<UpdateOneNovelPageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateOneNovelPageCommandHandler> _logger;

        public UpdateOneNovelPageCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateOneNovelPageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(UpdateOneNovelPageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode.GetByIdAsync(command.SeriesEpisodeId, cancellationToken);
            if (seriesEpisode == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var seriesEpisodeNovelPagesEntity = await _unitOfWork.SeriesEpisodesPage.GetByIdAsync(command.SeriesEpisodeId, cancellationToken);
            if (seriesEpisodeNovelPagesEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfası"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodeNovelPagesEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfası"), ApplicationMessages.ErrorDefaultNotFound);
            }

            seriesEpisodeNovelPagesEntity.PageContent = command.NovelPage.PageContent;
            seriesEpisodeNovelPagesEntity.PageNo = command.NovelPage.PageNo;

            await _unitOfWork.SeriesEpisodesPage.UpdateAsync(seriesEpisodeNovelPagesEntity, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
