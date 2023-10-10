using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using MangaFatihi.Shared.Domain.Entities.Teams;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Commands
{
    public class CreateTeamCommandHandler : ICommandHandler<CreateTeamCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            var teamEntity = await _unitOfWork.Team.AddAsyncReturnEntity(new()
            {
                Name = command.Name,
                WebSiteUrl = command.WebSiteUrl,
                Description = command.Description,
                ProfileImageUrl = command.ProfileImageUrl,
                BackgroundImageUrl = command.BackgroundImageUrl,

            }, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

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

            return new SuccessDataResult<object>(ApplicationMessages.SuccessAddProcess.GetMessage(), ApplicationMessages.SuccessAddProcess);
        }

    }
}
