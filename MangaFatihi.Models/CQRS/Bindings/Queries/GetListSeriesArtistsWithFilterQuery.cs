using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Base;
using MangaFatihi.Models.CQRS.DTOs.Queries;
using Mediator;

namespace MangaFatihi.Models.CQRS.Bindings.Queries
{
    public class GetListSeriesArtistsWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesArtistsWithFilterQueryDto>>
    {
        /// <summary>
        /// Sanatçının adına göre filtreleme
        /// </summary>
        public string? FullName { get; set; }

    }
}
