using MangaFatihi.Shared.Models.API.Commons.SeriesEpisodes;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

public class GetListSeriesEpisodesWithFilterQueryDto
{
    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin listesi
    /// </summary>
    public List<SeriesEpisodeListModel> List { get; set; } = new();

    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin sayısı
    /// </summary>
    public int TotalCount { get; set; } = 0;
}
