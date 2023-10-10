using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Constants;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class EditSeriesEpisodeCommandHandler : ICommandHandler<EditSeriesEpisodeCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditSeriesEpisodeCommandHandler> _logger;

        public EditSeriesEpisodeCommandHandler(IUnitOfWork unitOfWork, ILogger<EditSeriesEpisodeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditSeriesEpisodeCommand command, CancellationToken cancellationToken)
        {
            var seriesEpisodeId = Guid.Parse(command.Id);

            var seriesEpisodeEntity = await _unitOfWork.SeriesEpisode.GetByIdAsync(seriesEpisodeId, cancellationToken);
            if (seriesEpisodeEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound, seriesEpisodeEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri Bölümü"), ApplicationMessages.ErrorDefaultNotFound);
            }

            //eğer seri numarası değiştirildi ise
            if (seriesEpisodeEntity.EpisodeNo != command.EpisodeNo)
            {
                //Daha öncesinden bölüm numası, bu ekip tarafından kullanıldı mı?
                var check = await _unitOfWork.SeriesEpisode.Find(i => i.IsActive && i.SeriesId == command.SeriesId && i.EpisodeNo == command.EpisodeNo && i.TeamId == command.TeamId).AnyAsync(cancellationToken);
                if (check)
                {
                    return new ErrorDataResult<object>(ApplicationMessages.ErrorSeriesEpisodeAlreadyAdded.GetMessage(), ApplicationMessages.ErrorSeriesEpisodeAlreadyAdded);
                }
            }

            #region İlişkilendirilecek Kullanıcılar

            var onlyUserIds = new List<Guid>();
            if (command.EditorUserId.HasValue)
            {
                onlyUserIds.Add(command.EditorUserId.Value);
            }
            if (command.TranslatorUserId.HasValue)
            {
                onlyUserIds.Add(command.TranslatorUserId.Value);
            }

            var users = await _unitOfWork.UserManager.Users.Where(i => i.IsActive && onlyUserIds.Contains(i.Id)).ToListAsync(cancellationToken);

            #endregion

            #region İlişkilendirilen seri

            var series = await _unitOfWork.Series.GetByIdAsync(command.SeriesId, cancellationToken);
            if (series == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri"), ApplicationMessages.ErrorDefaultNotFound, series);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Seri"), ApplicationMessages.ErrorDefaultNotFound);
            }

            #endregion

            #region İlişkilendirilen Ekip (varsa)

            var team = await _unitOfWork.Team.GetByIdAsync(command.TeamId.HasValue ? command.TeamId.Value : Guid.NewGuid(), cancellationToken);

            #endregion

            seriesEpisodeEntity.TeamId = team?.Id;

            seriesEpisodeEntity.EditorUserId = users.FirstOrDefault(i => i.Id == command.EditorUserId)?.Id;
            seriesEpisodeEntity.TranslatorUserId = users.FirstOrDefault(i => i.Id == command.TranslatorUserId)?.Id;

            seriesEpisodeEntity.SeriesId = series.Id;

            seriesEpisodeEntity.IsOnAir = command.IsOnAir;
            seriesEpisodeEntity.Note = command.Note;
            seriesEpisodeEntity.Title = command.Title;
            seriesEpisodeEntity.StaticSeriesEpisodeTypeId = command.SeriesEpisodeTypeId;

            await _unitOfWork.SeriesEpisode.UpdateAsync(seriesEpisodeEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
