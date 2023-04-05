using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Commands
{
    /// <summary>
    /// 
    /// </summary>
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
