using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual IList<SeriesAndSeriesCategory> SeriesAndSeriesCategories { get; set; }

    }
}
