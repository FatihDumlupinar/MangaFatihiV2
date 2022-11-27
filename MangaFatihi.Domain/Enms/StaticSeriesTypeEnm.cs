using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Enms
{
    public enum StaticSeriesTypeEnm
    {
        [Display(Name ="Manga")]
        manga = 1,
        
        [Display(Name = "Webtoon")]
        webtoon = 2,
        
        [Display(Name = "Manhua")]
        manhua = 3,
        
        [Display(Name = "Manhwa")]
        manhwa = 4,

        [Display(Name = "Novel")]
        novel = 5,


    }
}
