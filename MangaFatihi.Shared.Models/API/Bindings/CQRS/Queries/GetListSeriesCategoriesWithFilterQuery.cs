using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.Base;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.API.Bindings.CQRS.Queries;

public class GetListSeriesCategoriesWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesCategoriesWithFilterQueryDto>>
{
}
