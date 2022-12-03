using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    /// <summary>
    /// Seri bölümlerinin türleri
    /// </summary>
    public enum StaticSeriesEpisodeTypeEnm
    {
        /// <summary>
        /// Görsel
        /// </summary>
        [Display(Name = "Görsel")]
        image = 1,

        /// <summary>
        /// Yazı
        /// </summary>
        [Display(Name = "Yazı")]
        text = 2,

    }
}
