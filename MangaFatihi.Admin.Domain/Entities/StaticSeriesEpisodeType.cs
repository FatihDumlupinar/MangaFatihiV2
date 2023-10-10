using MangaFatihi.Admin.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class StaticSeriesEpisodeType : StaticBaseEntity
    {
        public virtual IList<SeriesEpisode> SeriesEpisodes { get; set; }
    }
}
