using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    public enum StaticSeriesTypeEnm
    {
        /// <summary>
        /// Manga
        /// </summary>
        [Display(Name ="Manga")]
        manga = 1,

        /// <summary>
        /// Webtoon
        /// </summary>
        [Display(Name = "Webtoon")]
        webtoon = 2,

        /// <summary>
        /// Manhua
        /// </summary>
        [Display(Name = "Manhua")]
        manhua = 3,

        /// <summary>
        /// Manhwa
        /// </summary>
        [Display(Name = "Manhwa")]
        manhwa = 4,

        /// <summary>
        /// Novel
        /// </summary>
        [Display(Name = "Novel")]
        novel = 5,


    }
}
