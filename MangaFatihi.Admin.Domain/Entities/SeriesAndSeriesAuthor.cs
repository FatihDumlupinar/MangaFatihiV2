using MangaFatihi.Admin.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesAndSeriesAuthor : BaseEntity
    {
        public Guid SeriesId { get; set; }

        public virtual Series Series { get; set; }


        public Guid SeriesAuthorId { get; set; }

        public virtual SeriesAuthor SeriesAuthor { get; set; }
    }
}
