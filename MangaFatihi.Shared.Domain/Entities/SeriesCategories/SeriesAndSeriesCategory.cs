using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaFatihi.Shared.Domain.Entities.SeriesCategories
{
    public class SeriesAndSeriesCategory : BaseEntity
    {
        public Guid SeriesId { get; set; }

        public virtual Series Series { get; set; }


        public Guid SeriesCategoryId { get; set; }

        public virtual SeriesCategory SeriesCategory { get; set; }
    }
}
