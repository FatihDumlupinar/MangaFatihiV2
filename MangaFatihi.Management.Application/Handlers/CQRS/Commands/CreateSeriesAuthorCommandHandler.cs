﻿using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using MangaFatihi.Shared.Domain.Entities.SeriesAuthors;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class CreateSeriesAuthorCommandHandler : ICommandHandler<CreateSeriesAuthorCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public CreateSeriesAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateSeriesAuthorCommand command, CancellationToken cancellationToken)
        {
            var seriesAuthorEntity = await _unitOfWork.SeriesAuthor.AddAsyncReturnEntity(new()
            {
                FullName = command.FullName,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            if (command.SeriesIds != null && command.SeriesIds.Any())
            {
                var series = await _unitOfWork.Series
                    .Find(i => i.IsActive && command.SeriesIds.Contains(i.Id))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync(cancellationToken);
                if (series.Any())
                {
                    var seriesAndSeriesAuthorsList = series.Select(i => new SeriesAndSeriesAuthor()
                    {
                        SeriesId = i.Id,
                        SeriesAuthorId = seriesAuthorEntity.Id,
                    });

                    await _unitOfWork.SeriesAndSeriesAuthor.AddRangeAsync(seriesAndSeriesAuthorsList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}