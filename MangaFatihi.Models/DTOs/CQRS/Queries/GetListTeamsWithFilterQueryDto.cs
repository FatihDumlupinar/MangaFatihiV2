using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Queries
{
    public class GetListTeamsWithFilterQueryDto
    {
        /// <summary>
        /// Filtreye göre veritabanında bulunan verilerin listesi
        /// </summary>
        public List<TeamsListModel> List { get; set; } = new();

        /// <summary>
        /// Filtreye göre veritabanında bulunan verilerin sayısı
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }
}
