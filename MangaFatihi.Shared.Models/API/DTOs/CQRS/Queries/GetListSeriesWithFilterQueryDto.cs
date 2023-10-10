using MangaFatihi.Shared.Models.API.Commons.Series;

namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Queries
{
    public class GetListSeriesWithFilterQueryDto
    {
        /// <summary>
        /// Filtreye göre veritabanında bulunan verilerin listesi
        /// </summary>
        public List<SeriesListModel> List { get; set; } = new();

        /// <summary>
        /// Filtreye göre veritabanında bulunan verilerin sayısı
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }

}
