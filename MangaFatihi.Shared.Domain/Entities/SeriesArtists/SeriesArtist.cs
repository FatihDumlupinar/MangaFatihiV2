using MangaFatihi.Shared.Domain.Common;

namespace MangaFatihi.Shared.Domain.Entities.SeriesArtists
{
    public class SeriesArtist : BaseEntity
    {
        public string FullName { get; set; }

        public virtual IList<SeriesAndSeriesArtist> SeriesAndSeriesArtists { get; set; }
    }
}
