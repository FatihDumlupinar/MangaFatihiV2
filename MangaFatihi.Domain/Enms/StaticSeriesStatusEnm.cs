using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    public enum StaticSeriesStatusEnm
    {
        [Display(Name = "Manga")]
        ongoing = 1,

        [Display(Name = "Manga")]
        stopped = 2,

        [Display(Name = "Manga")]
        finished = 3,

        [Display(Name = "Manga")]
        cancelled = 4,

    }
}
