using MangaFatihi.Shared.Domain.Common;

namespace MangaFatihi.Shared.Domain.Entities.SeriesCategories
{
    public class SeriesCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual IList<SeriesAndSeriesCategory> SeriesAndSeriesCategories { get; set; }

    }
}
