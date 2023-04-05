using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Commands;

public class GetSeriesCategoryInformationQueryDto
{
    /// <summary>
    /// Kategorinin Unique Id'si
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Kategorinin adı
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Kategori ile ilişkilendirilen serilerin listesi
    /// </summary>
    public List<SmallSeriesListModel> SeriesList { get; set; }
}
