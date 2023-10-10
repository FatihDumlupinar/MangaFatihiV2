using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesEnty;

namespace MangaFatihi.Shared.Domain.Entities.Statics
{
    public class StaticSeriesStatus : StaticBaseEntity
    {
        public virtual IList<Series> Series { get; set; }
    }
}
