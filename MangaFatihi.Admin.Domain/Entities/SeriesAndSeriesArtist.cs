using MangaFatihi.Admin.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesAndSeriesArtist : BaseEntity
    {
        public Guid SeriesId { get; set; }
        
        public virtual Series Series { get; set; }


        public Guid SeriesArtistId { get; set; }

        public virtual SeriesArtist SeriesArtist { get; set; }

    }
}
