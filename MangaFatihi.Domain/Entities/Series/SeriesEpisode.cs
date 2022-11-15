using MangaFatihi.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MangaFatihi.Domain.Entities
{
    public class SeriesEpisode : BaseEntity
    {
        [MinLength(1)]
        public int EpisodeNo { get; set; } = 1;

        public string? Title { get; set; }
        
        public uint ViewsCount { get; set; } = 0;
        
        public string? Note { get; set; }
        
        public string? FileSizeMb { get; set; }

        /// <summary>
        /// Yayında mı?
        /// </summary>
        public bool IsOnAir { get; set; } = true;

        public virtual AppUser? EditorUser { get; set; }

        public virtual AppUser? TranslatorUser { get; set; }

        public virtual Series Series { get; set; }
        public virtual StaticSeriesEpisodeType StaticSeriesEpisodeTypes { get; set; }

        public virtual IList<SeriesEpisodesPage> SeriesEpisodesPages { get; set; }
    }
}
