using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Application.Models.DTOs.Queries
{
    public class SeriesGetListWithFilterQueryDto
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
