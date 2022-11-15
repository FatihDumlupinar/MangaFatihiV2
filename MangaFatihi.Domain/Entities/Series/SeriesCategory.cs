using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesCategory : BaseEntity
    {
        public string Name { get; set; }

        //Çoka çok ilişkiler
        public virtual IList<Series> Series { get; set; }

    }
}
