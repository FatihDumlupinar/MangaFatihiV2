using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.Extensions.Logging;
using MangaFatihi.Shared.Models.DataResults;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class CreateOneNovelPageCommandHandler : ICommandHandler<CreateOneNovelPageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOneNovelPageCommandHandler> _logger;

        public CreateOneNovelPageCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateOneNovelPageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateOneNovelPageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode.GetByIdAsync(command.SeriesEpisodeId, cancellationToken);
            if (seriesEpisode == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.SeriesEpisodesPage.AddAsync(new() 
            {
                PageContent = command.NovelPage.PageContent,
                PageNo = command.NovelPage.PageNo,
                SeriesEpisodesId = seriesEpisode.Id,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }
    }
}
