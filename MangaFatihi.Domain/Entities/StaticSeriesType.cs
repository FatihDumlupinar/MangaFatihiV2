using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class StaticSeriesType : StaticBaseEntity
    {

        public virtual IList<Series> Series { get; set; }
    }
}
