namespace MangaFatihi.Models.Bindings.CQRS.Base;

/// <summary>
/// Temel sayfalama ve filter için kullanılan parametreler
/// </summary>
public class BasePaginationFilterModel
{
    /// <summary>
    /// Gösterilecek veri sayısı, eğer 0 gönderilirse bütün verileri getirir
    /// </summary>
    public int PageLength { get; set; } = 10;

    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// Sıralanacak kolon parametresi
    /// </summary>
    public string OrderBy { get; set; } = "";

    /// <summary>
    /// Sıralama türü (Eğer true ise; Çoktan aza, false ise azdan çoka)
    /// </summary>
    public bool OrderByDesc { get; set; } = true;

    /// <summary>
    /// Belirli kolonlarda, veriyi aramak için
    /// </summary>
    public string Search { get; set; } = "";
}
