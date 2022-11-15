using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        public string? IpAddress { get; set; }

        public string? AccessToken { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
