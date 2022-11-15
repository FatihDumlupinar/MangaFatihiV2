using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesAuthor : BaseEntity
    {
        public string FullName { get; set; }

        //Çoka çok ilişkiler
        public virtual IList<Series> Series { get; set; }
    }
}
