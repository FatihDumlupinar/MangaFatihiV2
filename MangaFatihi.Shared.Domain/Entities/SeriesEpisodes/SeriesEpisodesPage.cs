using MangaFatihi.Shared.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Shared.Domain.Entities.SeriesEpisodes
{
    public class SeriesEpisodesPage : BaseEntity
    {
        [MinLength(1)]
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// Eğer serinin bölümleri resim ise, resim adres yolu
        /// </summary>
        public string? PageImageUrl { get; set; }

        /// <summary>
        /// Eğer serinin bölümleri yazı ise, yazının içeriği
        /// </summary>
        public string? PageContent { get; set; }

        public virtual SeriesEpisode SeriesEpisodes { get; set; }
        public Guid SeriesEpisodesId { get; set; }
    }
}
