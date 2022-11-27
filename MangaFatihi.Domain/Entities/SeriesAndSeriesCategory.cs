using MangaFatihi.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesAndSeriesCategory : BaseEntity
    {
        public Guid SeriesId { get; set; }

        public virtual Series Series { get; set; }


        public Guid SeriesCategoryId { get; set; }

        public virtual SeriesCategory SeriesCategory { get; set; }
    }
}
