using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class TeamAndAppUser : BaseEntity
    {
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
