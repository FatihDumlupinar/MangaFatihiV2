using MangaFatihi.Application.CQRS.Base;
using MangaFatihi.Application.Models.Base;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Enms;
using MediatR;

namespace MangaFatihi.Application.CQRS.Queries
{
    public class SeriesGetListWithFilterQuery : BasePaginationFilterModel, IRequest<DataResult<SeriesGetListWithFilterQueryDto>>
    {
        /// <summary>
        /// Serinin durumu
        /// </summary>
        public List<StaticSeriesStatusEnm> SeriesStatusIds { get; set; } = new();

        /// <summary>
        /// Serinin türü
        /// </summary>
        public List<StaticSeriesTypeEnm> SeriesTypeIds { get; set; } = new();

        /// <summary>
        /// Serinin sanatçıları
        /// </summary>
        public List<Guid> SeriesArtistIds { get; set; } = new();

        /// <summary>
        /// Serinin yazarları
        /// </summary>
        public List<Guid> SeriesAuthorIds { get; set; } = new();

        /// <summary>
        /// Serinin kategorileri
        /// </summary>
        public List<Guid> SeriesCategoryIds { get; set; } = new();


    }
}
