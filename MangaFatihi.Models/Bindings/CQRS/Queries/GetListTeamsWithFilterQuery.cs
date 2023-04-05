using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Base;
using MangaFatihi.Models.DTOs.CQRS.Commands;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetListTeamsWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListTeamsWithFilterQueryDto>>
{
    /// <summary>
    /// Üyelere göre arama
    /// </summary>
    public List<Guid> UserList { get; set; } = new();

}
