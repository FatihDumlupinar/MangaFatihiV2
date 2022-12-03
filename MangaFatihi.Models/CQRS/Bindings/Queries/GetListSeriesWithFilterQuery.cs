using FluentValidation;
using MangaFatihi.Application.Models.DTOs.Queries;
using MangaFatihi.Domain.Enms;
using MangaFatihi.Models.Base;
using MangaFatihi.Models.CQRS.Bindings.Base;
using Mediator;

namespace MangaFatihi.Models.CQRS.Bindings.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class GetListSeriesWithFilterQuery : BasePaginationFilterModel, IQuery<DataResult<GetListSeriesWithFilterQueryDto>>
    {
        /// <summary>
        /// Serinin durumu
        /// </summary>
        public List<StaticSeriesStatusEnm>? SeriesStatusIds { get; set; } = new();

        /// <summary>
        /// Serinin türü
        /// </summary>
        public List<StaticSeriesTypeEnm>? SeriesTypeIds { get; set; } = new();

        /// <summary>
        /// Serinin sanatçıları
        /// </summary>
        public List<Guid>? SeriesArtistIds { get; set; } = new();

        /// <summary>
        /// Serinin yazarları
        /// </summary>
        public List<Guid>? SeriesAuthorIds { get; set; } = new();

        /// <summary>
        /// Serinin kategorileri
        /// </summary>
        public List<Guid>? SeriesCategoryIds { get; set; } = new();


    }

    public class GetListSeriesWithFilterQueryValidator : AbstractValidator<GetListSeriesWithFilterQuery>
    {
        public GetListSeriesWithFilterQueryValidator()
        {

        }

    }
}
