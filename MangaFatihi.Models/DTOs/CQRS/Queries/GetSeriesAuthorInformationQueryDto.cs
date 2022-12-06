using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Queries;

public class GetSeriesAuthorInformationQueryDto
{
    /// <summary>
    /// Yazarın Unique Id'si
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Yazarın adı soyadı
    /// </summary>
    public string FullName { get; set; } = "";

    /// <summary>
    /// Yazar ile ilişkilendirilen serilerin listesi
    /// </summary>
    public List<SmallSeriesListModel> SeriesList { get; set; }
}
