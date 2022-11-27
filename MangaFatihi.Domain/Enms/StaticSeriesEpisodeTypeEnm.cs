using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    public enum StaticSeriesEpisodeTypeEnm
    {
        [Display(Name = "Görsel")]
        image = 1,

        [Display(Name = "Yazı")]
        text = 2,

    }
}
