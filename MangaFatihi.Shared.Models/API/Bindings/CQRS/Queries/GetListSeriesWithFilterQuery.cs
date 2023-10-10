using FluentValidation;
using Mediator;
using MangaFatihi.Shared.Models.Enms;
using MangaFatihi.Shared.Models.Bindings.Base;
using MangaFatihi.Shared.Models.DataResults;
using MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

namespace MangaFatihi.Shared.Models.Bindings.CQRS.Queries;

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
