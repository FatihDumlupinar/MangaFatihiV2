using MangaFatihi.Domain.Entities;
using MangaFatihi.Shared.Domain.Common;

namespace MangaFatihi.Shared.Domain.Entities.Teams
{
    public class TeamAndAppUser : BaseEntity
    {
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
