using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Entities;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class CreateSeriesArtistCommandHandler : ICommandHandler<CreateSeriesArtistCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public CreateSeriesArtistCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateSeriesArtistCommand command, CancellationToken cancellationToken)
        {
            var seriesArtistEntity = await _unitOfWork.SeriesArtist.AddAsyncReturnEntity(new()
            {
                FullName = command.FullName,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            if (command.SeriesIds != null && command.SeriesIds.Any())
            {
                var series = await _unitOfWork.Series.Find(i => i.IsActive && command.SeriesIds.Contains(i.Id)).ToListAsync(cancellationToken);
                if (series.Any())
                {
                    var seriesAndSeriesArtistsList = series.Select(i => new SeriesAndSeriesArtist()
                    {
                        Series = i,
                        SeriesArtist = seriesArtistEntity
                    });

                    await _unitOfWork.SeriesAndSeriesArtist.AddRangeAsync(seriesAndSeriesArtistsList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}
