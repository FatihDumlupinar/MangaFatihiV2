using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesEpisodes;

namespace MangaFatihi.Shared.Domain.Entities.Statics
{
    public class StaticSeriesEpisodeType : StaticBaseEntity
    {
        public virtual IList<SeriesEpisode> SeriesEpisodes { get; set; }
    }
}
