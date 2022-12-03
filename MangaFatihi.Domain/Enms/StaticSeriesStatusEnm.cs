using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    /// <summary>
    /// Serinin Durumları
    /// </summary>
    public enum StaticSeriesStatusEnm
    {
        /// <summary>
        /// Devam Ediyor
        /// </summary>
        [Display(Name = "Devam Ediyor")]
        ongoing = 1,

        /// <summary>
        /// Durduruldu
        /// </summary>
        [Display(Name = "Durduruldu")]
        stopped = 2,

        /// <summary>
        /// Bitti
        /// </summary>
        [Display(Name = "Bitti")]
        finished = 3,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        [Display(Name = "İptal Edildi")]
        cancelled = 4,

    }
}
