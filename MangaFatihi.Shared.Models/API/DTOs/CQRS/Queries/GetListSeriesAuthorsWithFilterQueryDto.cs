using MangaFatihi.Shared.Models.API.Commons.SeriesAuthors;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

public class GetListSeriesAuthorsWithFilterQueryDto
{
    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin listesi
    /// </summary>
    public List<SeriesAuthorListModel> List { get; set; } = new();

    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin sayısı
    /// </summary>
    public int TotalCount { get; set; } = 0;
}
