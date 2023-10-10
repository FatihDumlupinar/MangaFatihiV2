using MangaFatihi.Shared.Domain.Entities.Teams;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using MangaFatihi.Shared.Models.Constants;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class EditTeamCommandHandler : ICommandHandler<EditTeamCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditTeamCommandHandler> _logger;

        public EditTeamCommandHandler(IUnitOfWork unitOfWork, ILogger<EditTeamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(EditTeamCommand command, CancellationToken cancellationToken)
        {
            var teamId = Guid.Parse(command.TeamId);

            var teamEntity = await _unitOfWork.Team.GetByIdAsync(teamId);
            if (teamEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound, teamEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound);
            }

            teamEntity.BackgroundImageUrl = command.BackgroundImageUrl;
            teamEntity.Description = command.Description;
            teamEntity.Name = command.Name;
            teamEntity.ProfileImageUrl = command.ProfileImageUrl;
            teamEntity.WebSiteUrl = command.WebSiteUrl;

            await _unitOfWork.Team.UpdateAsync(teamEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            var teamAndUserList_old = await _unitOfWork.TeamAndAppUser
                .Find(i => i.IsActive && i.TeamId == teamId)
                .ToListAsync(cancellationToken);
            if (teamAndUserList_old.Any())
            {
                await _unitOfWork.TeamAndAppUser.DeleteRangeAsync(teamAndUserList_old);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            if (command.UserList != null && command.UserList.Any())
            {
                var users = await _unitOfWork.UserManager.Users
                    .Where(i => i.IsActive && command.UserList.Contains(i.Id))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync(cancellationToken);
                if (users.Any())
                {
                    var teamAndUserList = users.Select(i => new TeamAndAppUser()
                    {
                        AppUserId = i.Id,
                        TeamId = teamEntity.Id,

                    });

                    await _unitOfWork.TeamAndAppUser.AddRangeAsync(teamAndUserList, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }

            }

            return new SuccessDataResult<object>(ApplicationMessages.SuccessUpdateProcess.GetMessage(), ApplicationMessages.SuccessUpdateProcess);
        }
    }
}
