using MangaFatihi.Shared.Models.API.Commons.Series;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

public class GetSeriesArtistInformationQueryDto
{
    /// <summary>
    /// Sanatçının Unique Id'si
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Sanatçının adı soyadı
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// Sanatçı ilişkilendirilen serilerin listesi
    /// </summary>
    public List<SmallSeriesListModel> SeriesList { get; set; } = new();

}
