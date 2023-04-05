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
    public class UpdateMultiNovelPageCommandHandler : ICommandHandler<UpdateMultiNovelPageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateMultiNovelPageCommandHandler> _logger;

        public UpdateMultiNovelPageCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateMultiNovelPageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(UpdateMultiNovelPageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode.GetByIdAsync(command.SeriesEpisodeId, cancellationToken);
            if (seriesEpisode == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var onlyNovelPageIds = command.NovelPages.Select(i => i.PageId).ToList();

            var seriesEpisodeNovelPagesEntities = await _unitOfWork.SeriesEpisodesPage
                .Find(i => i.IsActive && i.PageContent != null && i.PageImageUrl == null && onlyNovelPageIds.Contains(i.Id))
                .ToListAsync(cancellationToken);
            if (seriesEpisodeNovelPagesEntities == default || !seriesEpisodeNovelPagesEntities.Any())
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfaları"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodeNovelPagesEntities);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümün Sayfaları"), ApplicationMessages.ErrorDefaultNotFound);
            }

            foreach (var seriesEpisodesPageEntity in seriesEpisodeNovelPagesEntities)
            {
                var novelPage = command.NovelPages.FirstOrDefault(i => i.PageId == seriesEpisodesPageEntity.Id);
                if (novelPage != null)
                {
                    seriesEpisodesPageEntity.PageContent = novelPage.PageContent;
                    seriesEpisodesPageEntity.PageNo = novelPage.PageNo;
                }
            }

            await _unitOfWork.SeriesEpisodesPage.UpdateRangeAsync(seriesEpisodeNovelPagesEntities, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
