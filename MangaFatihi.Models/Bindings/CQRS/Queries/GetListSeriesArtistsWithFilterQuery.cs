using MangaFatihi.Models.Base;
using MangaFatihi.Models.Bindings.CQRS.Base;
using MangaFatihi.Models.DTOs.CQRS.Queries;
using Mediator;

namespace MangaFatihi.Models.Bindings.CQRS.Queries;

public class GetListSeriesArtistsWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesArtistsWithFilterQueryDto>>
{

}
