using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Commands;

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
