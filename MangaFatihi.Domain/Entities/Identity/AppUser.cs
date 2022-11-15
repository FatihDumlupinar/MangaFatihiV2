using MangaFatihi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace MangaFatihi.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public string? About { get; set; }
        public string? ProfileImg { get; set; }

        /// <summary>
        /// Cinsiyet: 1 erkek 2 kadın, 0 belirtilmedi
        /// </summary>
        public int? Gender { get; set; }

        public virtual StaticCountry? StaticCountry { get; set; }
        public virtual StaticCity? StaticCity { get; set; }
        public virtual Team? Team { get; set; }

        public virtual IList<RefreshToken> RefreshTokens { get; set; }

    }
}
