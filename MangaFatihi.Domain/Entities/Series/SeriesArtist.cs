using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesArtist : BaseEntity
    {
        public string FullName { get; set; }

        //Çoka çok ilişkiler
        public virtual IList<Series> Series { get; set; }

    }
}
