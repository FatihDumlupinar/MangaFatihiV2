using MangaFatihi.Shared.Models.API.Commons.SeriesArtists;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries;

public class GetListSeriesArtistsWithFilterQueryDto
{
    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin listesi
    /// </summary>
    public List<SeriesArtistListModel> List { get; set; } = new();

    /// <summary>
    /// Filtreye göre veritabanında bulunan verilerin sayısı
    /// </summary>
    public int TotalCount { get; set; } = 0;

}
