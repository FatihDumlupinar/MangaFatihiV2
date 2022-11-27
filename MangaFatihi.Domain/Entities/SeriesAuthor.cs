using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesAuthor : BaseEntity
    {
        public string FullName { get; set; }

        public virtual IList<SeriesAndSeriesAuthor> SeriesAndSeriesAuthors { get; set; }
    }
}
