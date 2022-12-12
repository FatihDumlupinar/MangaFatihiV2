using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class CreateSeriesEpisodeCommandHandler : ICommandHandler<CreateSeriesEpisodeCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSeriesEpisodeCommandHandler> _logger;

        public CreateSeriesEpisodeCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateSeriesEpisodeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateSeriesEpisodeCommand command, CancellationToken cancellationToken)
        {
            //Daha öncesinden bölüm numası, bu ekip tarafından kullanıldı mı?
            var check = await _unitOfWork.SeriesEpisode.Find(i => i.IsActive && i.SeriesId == command.SeriesId && i.EpisodeNo == command.EpisodeNo && i.TeamId == command.TeamId).AnyAsync(cancellationToken);
            if (check)
            {
                return new ErrorDataResult<object>(ApplicationMessages.ErrorSeriesEpisodeAlreadyAdded.GetMessage(), ApplicationMessages.ErrorSeriesEpisodeAlreadyAdded);
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

            var seriesEpisodeEntity = await _unitOfWork.SeriesEpisode.AddAsyncReturnEntity(new()
            {
                EditorUserId = users.FirstOrDefault(i => i.Id == command.EditorUserId)?.Id,
                EpisodeNo = command.EpisodeNo,
                IsOnAir = command.IsOnAir,
                Note = command.Note,
                SeriesId = series.Id,
                StaticSeriesEpisodeTypeId = (int)command.SeriesEpisodeType,
                TeamId = team?.Id,
                Title = command.Title,
                TranslatorUserId = users.FirstOrDefault(i => i.Id == command.TranslatorUserId)?.Id,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }
    }
}
