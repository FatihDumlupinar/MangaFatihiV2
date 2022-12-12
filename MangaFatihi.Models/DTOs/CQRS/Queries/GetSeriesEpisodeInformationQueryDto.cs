using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.DTOs.CQRS.Queries
{
    public class GetSeriesEpisodeInformationQueryDto
    {
        /// <summary>
        /// Bölümün Enique Id'si
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Bölüm numarası
        /// </summary>
        public int EpisodeNo { get; set; } = 1;

        /// <summary>
        /// Bölüm başlığı
        /// </summary>
        public string? Title { get; set; } = "";

        /// <summary>
        /// Görüntülenme sayısı
        /// </summary>
        public uint ViewsCount { get; set; } = 0;

        /// <summary>
        /// Bölüm için not
        /// </summary>
        public string? Note { get; set; } = "";

        /// <summary>
        /// Bölümün dosya boyutu
        /// </summary>
        public string? FileSizeMb { get; set; } = "";

        /// <summary>
        /// Yayında mı?
        /// </summary>
        public bool IsOnAir { get; set; } = true;

        /// <summary>
        /// Seriyi Çeviren Ekip
        /// </summary>
        public string? Team { get; set; } = "";

        /// <summary>
        /// Editor
        /// </summary>
        public string? EditorUser { get; set; } = "";
        
        /// <summary>
        /// Çevirmen
        /// </summary>
        public string? TranslatorUser { get; set; } = "";

        /// <summary>
        /// Bölümün bağlı olduğu serinin Unique Id'si
        /// </summary>
        public Guid SeriesId { get; set; }

        /// <summary>
        /// Bölümün bağlı olduğu seri
        /// </summary>
        public string Series { get; set; } = "";

        /// <summary>
        /// Bölümün Türü
        /// </summary>
        public string SeriesEpisodeType { get; set; } = "";
        
        /// <summary>
        /// Bölümün Türünün Id'si
        /// </summary>
        public int SeriesEpisodeTypeId { get; set; } = 0;

        /// <summary>
        /// Bölümün Sayfaları
        /// </summary>
        public List<SeriesEpisodesDetailPageListModel> SeriesEpisodesPageList { get; set; } = new();

    }
}
