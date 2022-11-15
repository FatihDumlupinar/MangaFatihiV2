﻿using MangaFatihi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Entities
{
    public class Team : BaseEntity
    {
        [Required]
        public string Name { get; set; } = "";

        public string? WebSiteUrl { get; set; }
        public string? BackgroundImageUrl { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Description { get; set; }

        //Çoka bir ilişkiler
        public virtual IList<AppUser> AppUser { get; set; }

    }
}
