using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class StaticSeriesStatus : StaticBaseEntity
    {
        public virtual IList<Series> Series { get; set; }
    }
}
