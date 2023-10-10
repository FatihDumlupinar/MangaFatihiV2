using MangaFatihi.Shared.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Shared.Domain.Entities.Teams
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; } = "";

        public string? WebSiteUrl { get; set; }
        public string? BackgroundImageUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Description { get; set; }

        public virtual IList<TeamAndAppUser> AppUser { get; set; }

    }
}
