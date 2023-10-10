using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;
using MangaFatihi.Shared.Models.Bindings.Base;
using MangaFatihi.Shared.Models.DataResults;
using Mediator;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

public class GetListSeriesEpisodesWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesEpisodesWithFilterQueryDto>>
{
    /// <summary>
    /// Seriye göre filtreleme
    /// </summary>
    public Guid? SeriesId { get; set; }

}
