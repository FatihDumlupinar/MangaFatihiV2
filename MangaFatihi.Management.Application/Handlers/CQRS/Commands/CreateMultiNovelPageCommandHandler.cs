﻿using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.Extensions.Logging;
using MangaFatihi.Shared.Domain.Entities.SeriesEpisodes;
using MangaFatihi.Shared.Models.DataResults;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class CreateMultiNovelPageCommandHandler : ICommandHandler<CreateMultiNovelPageCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateMultiNovelPageCommandHandler> _logger;

        public CreateMultiNovelPageCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateMultiNovelPageCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateMultiNovelPageCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisode = await _unitOfWork.SeriesEpisode.GetByIdAsync(command.SeriesEpisodeId, cancellationToken);
            if (seriesEpisode == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisode);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var seriesEpisodePageListModel = command.NovelPages.Select(i => new SeriesEpisodesPage()
            {
                PageContent = i.PageContent,
                PageNo = i.PageNo,
                SeriesEpisodesId = seriesEpisode.Id,

            });

            await _unitOfWork.SeriesEpisodesPage.AddRangeAsync(seriesEpisodePageListModel, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }
    }
}