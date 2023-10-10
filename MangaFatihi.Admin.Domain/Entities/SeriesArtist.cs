using MangaFatihi.Admin.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesArtist : BaseEntity
    {
        public string FullName { get; set; }

        public virtual IList<SeriesAndSeriesArtist> SeriesAndSeriesArtists { get; set; }
    }
}
