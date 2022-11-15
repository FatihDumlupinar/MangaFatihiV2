using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateUserId { get; set; }

    }
}
