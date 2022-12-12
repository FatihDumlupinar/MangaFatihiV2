namespace MangaFatihi.Models.Commonns;

/// <summary>
/// Bölüm detayında ki bölümün sayfaları
/// </summary>
public class SeriesEpisodesDetailPageListModel
{
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// Eğer serinin bölümleri resim ise, resim adres yolu
    /// </summary>
    public string? PageImageUrl { get; set; } = "";

    /// <summary>
    /// Eğer serinin bölümleri yazı ise, yazının içeriği
    /// </summary>
    public string? PageContent { get; set; } = "";

}
