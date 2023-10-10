using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaFatihi.Shared.Domain.Entities.SeriesAuthors
{
    public class SeriesAndSeriesAuthor : BaseEntity
    {
        public Guid SeriesId { get; set; }

        public virtual Series Series { get; set; }


        public Guid SeriesAuthorId { get; set; }

        public virtual SeriesAuthor SeriesAuthor { get; set; }
    }
}
