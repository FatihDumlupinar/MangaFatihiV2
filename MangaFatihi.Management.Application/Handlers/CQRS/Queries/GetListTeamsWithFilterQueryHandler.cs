using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Models.API.Commons.Teams;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.CQRS.Queries;
using MangaFatihi.Shared.Models.Constants;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace MangaFatihi.Management.Application.Handlers.CQRS.Queries
{
    public class GetListTeamsWithFilterQueryHandler : IQueryHandler<GetListTeamsWithFilterQuery, DataResult<GetListTeamsWithFilterQueryDto>>
    {
        #region Ctor&Fields

        private readonly IUnitOfWork _unitOfWork;

        public GetListTeamsWithFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async ValueTask<DataResult<GetListTeamsWithFilterQueryDto>> Handle(GetListTeamsWithFilterQuery query, CancellationToken cancellationToken)
        {
            var iQuerayble = _unitOfWork.Team.Find(i => i.IsActive);

            #region Filters

            if (!string.IsNullOrEmpty(query.Search))
            {
                iQuerayble = iQuerayble.Where(x => x.Name.Contains(query.Search));
            }

            #endregion

            #region Sıralama

            query.OrderBy = query.OrderBy.ToLower().Trim();

            switch (query.OrderBy)
            {
                case "id":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.Id) : iQuerayble.OrderBy(i => i.Id);
                    break;

                case "name":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.Name) : iQuerayble.OrderBy(i => i.Name);
                    break;

                case "createdate":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.CreateDate) : iQuerayble.OrderBy(i => i.CreateDate);
                    break;

                case "updatedate":
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.UpdateDate) : iQuerayble.OrderBy(i => i.UpdateDate);
                    break;

                default:
                    iQuerayble = query.OrderByDesc ? iQuerayble.OrderByDescending(i => i.CreateDate) : iQuerayble.OrderBy(i => i.CreateDate);
                    break;
            }

            #endregion

            var totalCount = await iQuerayble.CountAsync(cancellationToken);

            #region Sayfalama

            if (query.PageLength != 0)
            {
                if (query.PageNo <= 0)
                {
                    query.PageNo = 1;
                }

                var skip = (query.PageNo - 1) * query.PageLength;

                iQuerayble = iQuerayble.Skip(skip).Take(query.PageLength);
            }

            #endregion

            var teams = await iQuerayble.AsNoTrackingWithIdentityResolution().ToListAsync(cancellationToken);

            #region Listenin diğer elemanları

            var onlyUserIds = new List<Guid>();
            foreach (var team in teams)
            {
                if (team.CreateUserId.HasValue)
                {
                    onlyUserIds.Add(team.CreateUserId.Value);
                }

                if (team.UpdateUserId.HasValue)
                {
                    onlyUserIds.Add(team.UpdateUserId.Value);
                }
            }

            var users = await _unitOfWork.UserManager.Users
                .Where(i => i.IsActive && onlyUserIds.Contains(i.Id))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync(cancellationToken);

            #endregion

            var returnModel = new GetListTeamsWithFilterQueryDto()
            {
                List = teams.Select(i => new TeamsListModel()
                {
                    Name = i.Name,
                    Id = i.Id,
                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate,
                    CreateUser = users.Find(x => x.Id == i.CreateUserId)?.FullName,
                    UpdateUser = users.Find(x => x.Id == i.UpdateUserId)?.FullName

                }).ToList(),

                TotalCount = totalCount

            };

            return new SuccessDataResult<GetListTeamsWithFilterQueryDto>(returnModel, ApplicationMessages.SuccessGetListProcess.GetMessage(), ApplicationMessages.SuccessGetListProcess);
        }
    }
}
