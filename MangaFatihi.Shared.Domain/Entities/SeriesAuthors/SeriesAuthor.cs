using MangaFatihi.Shared.Domain.Common;

namespace MangaFatihi.Shared.Domain.Entities.SeriesAuthors
{
    public class SeriesAuthor : BaseEntity
    {
        public string FullName { get; set; }

        public virtual IList<SeriesAndSeriesAuthor> SeriesAndSeriesAuthors { get; set; }
    }
}
