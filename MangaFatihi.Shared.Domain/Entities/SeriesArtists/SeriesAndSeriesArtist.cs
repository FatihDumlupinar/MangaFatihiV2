using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;

namespace MangaFatihi.Shared.Domain.Entities.SeriesArtists
{
    public class SeriesAndSeriesArtist : BaseEntity
    {
        public Guid SeriesId { get; set; }

        public virtual Series Series { get; set; }


        public Guid SeriesArtistId { get; set; }

        public virtual SeriesArtist SeriesArtist { get; set; }

    }
}
