using MangaFatihi.Shared.Domain.Entities.SeriesArtists;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Constants;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class EditSeriesArtistCommandHandler : ICommandHandler<EditSeriesArtistCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditSeriesArtistCommandHandler> _logger;

        public EditSeriesArtistCommandHandler(IUnitOfWork unitOfWork, ILogger<EditSeriesArtistCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditSeriesArtistCommand command, CancellationToken cancellationToken)
        {
            var seriesArtistId = Guid.Parse(command.Id);

            var seriesArtistEntity = await _unitOfWork.SeriesArtist.GetByIdAsync(seriesArtistId, cancellationToken);
            if (seriesArtistEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound, seriesArtistEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Sanatçısı"), ApplicationMessages.ErrorDefaultNotFound);
            }

            seriesArtistEntity.FullName = command.FullName;

            await _unitOfWork.SeriesArtist.UpdateAsync(seriesArtistEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            //Eski olanları sil
            var seriesAndSeriesArtistList_old = await _unitOfWork.SeriesAndSeriesArtist
                .Find(i => i.IsActive && i.SeriesArtistId == seriesArtistId)
                .ToListAsync(cancellationToken);
            if (seriesAndSeriesArtistList_old.Any())
            {
                await _unitOfWork.SeriesAndSeriesArtist.DeleteRangeAsync(seriesAndSeriesArtistList_old, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

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
                        SeriesArtistId = seriesArtistEntity.Id
                    });

                    //Yenileri ekle
                    await _unitOfWork.SeriesAndSeriesArtist.AddRangeAsync(seriesAndSeriesArtistsList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }

            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }

    }
}
