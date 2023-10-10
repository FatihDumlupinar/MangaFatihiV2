using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using MangaFatihi.Shared.Domain.Entities.SeriesCategories;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class CreateSeriesCategoryCommandHandler : ICommandHandler<CreateSeriesCategoryCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public CreateSeriesCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateSeriesCategoryCommand command, CancellationToken cancellationToken)
        {
            var seriesCategoryEntity = await _unitOfWork.SeriesCategory.AddAsyncReturnEntity(new()
            {
                Name = command.Name,

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
                    var seriesAndSeriesCategoriesList = series.Select(i => new SeriesAndSeriesCategory()
                    {
                        SeriesId = i.Id,
                        SeriesCategoryId = seriesCategoryEntity.Id,
                    });

                    await _unitOfWork.SeriesAndSeriesCategory.AddRangeAsync(seriesAndSeriesCategoriesList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }
    }
}
