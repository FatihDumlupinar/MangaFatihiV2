using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Commands
{
    public class DeleteTeamCommandHandler : ICommandHandler<DeleteTeamCommand, DataResult<object>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTeamCommandHandler> _logger;

        public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteTeamCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<object>> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            var teamId = Guid.Parse(command.TeamId);

            var teamEntity = await _unitOfWork.Team.GetByIdAsync(teamId);
            if (teamEntity == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound, teamEntity);

                return new NotFoundDataResult<object>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound);
            }

            await _unitOfWork.Team.DeleteAsync(teamEntity);

            var teamAndUserList_old = await _unitOfWork.TeamAndAppUser
               .Find(i => i.IsActive && i.TeamId == teamId)
               .ToListAsync(cancellationToken);
            if (teamAndUserList_old.Any())
            {
                await _unitOfWork.TeamAndAppUser.DeleteRangeAsync(teamAndUserList_old);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return new SuccessDataResult<object>(ApplicationMessages.SuccessDeleteProcess.GetMessage(), ApplicationMessages.SuccessDeleteProcess);
        }

    }
}
