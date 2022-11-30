using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
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
