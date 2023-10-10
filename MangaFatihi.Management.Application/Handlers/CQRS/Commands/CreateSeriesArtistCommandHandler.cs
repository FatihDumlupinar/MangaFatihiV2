using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using MangaFatihi.Shared.Domain.Entities.SeriesArtists;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
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
                var series = await _unitOfWork.Series
                    .Find(i => i.IsActive && command.SeriesIds.Contains(i.Id))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync(cancellationToken);
                if (series.Any())
                {
                    var seriesAndSeriesArtistsList = series.Select(i => new SeriesAndSeriesArtist()
                    {
                        SeriesId = i.Id,
                        SeriesArtistId = seriesArtistEntity.Id,
                    });

                    await _unitOfWork.SeriesAndSeriesArtist.AddRangeAsync(seriesAndSeriesArtistsList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}
