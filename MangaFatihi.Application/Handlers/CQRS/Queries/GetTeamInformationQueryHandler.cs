using MangaFatihi.Domain.Constants;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Queries;
using MangaFatihi.Models.Commonns;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MangaFatihi.Application.Handlers.CQRS.Queries
{
    public class GetTeamInformationQueryHandler : IQueryHandler<GetTeamInformationQuery, DataResult<GetTeamInformationQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTeamInformationQueryHandler> _logger;

        public GetTeamInformationQueryHandler(IUnitOfWork unitOfWork, ILogger<GetTeamInformationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        public async ValueTask<DataResult<GetTeamInformationQueryDto>> Handle(GetTeamInformationQuery query, CancellationToken cancellationToken)
        {
            var teamId = Guid.Parse(query.TeamId);

            var team = await _unitOfWork.Team
                .Find(i => i.IsActive && i.Id == teamId)
                .Include(i => i.AppUser.Where(x => x.IsActive)).ThenInclude(i => i.AppUser)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);
            if (team == default)
            {
                _logger.LogError(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound, team);

                return new NotFoundDataResult<GetTeamInformationQueryDto>(string.Format(ApplicationMessages.ErrorDefaultNotFound.GetMessage(), "Takım"), ApplicationMessages.ErrorDefaultNotFound);
            }

            var returnModel = new GetTeamInformationQueryDto()
            {
                Id = teamId,
                Name = team.Name,
                UserList = team.AppUser.Select(i => new TeamUserListModel()
                {
                    FullName = i.AppUser.FullName,
                    UserId = i.Id

                }).ToList()

            };

            return new SuccessDataResult<GetTeamInformationQueryDto>(returnModel, ApplicationMessages.SuccessGetDetailsProcess.GetMessage(), ApplicationMessages.SuccessGetDetailsProcess);
        }
    }
}
