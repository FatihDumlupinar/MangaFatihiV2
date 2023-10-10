using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Shared.Models.Enms
{
    /// <summary>
    /// Uygulama içerisindeki roller
    /// </summary>
    public enum StaticRolesEnm
    {
        [Display(Name = "Yönetici")]
        Admin = 1,

        [Display(Name = "Moderatör")]
        Moderator,

        [Display(Name = "Takım Lideri")]
        Team_Leader,

        [Display(Name = "Editör")]
        Editor,

        [Display(Name = "Çevirmen")]
        Translator,

    }
}
