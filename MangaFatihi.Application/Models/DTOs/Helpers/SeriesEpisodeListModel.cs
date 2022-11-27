namespace MangaFatihi.Application.Models.DTOs.Helpers
{
    public class SeriesEpisodeListModel
    {
        /// <summary>
        /// bölümünün unique Id'si
        /// </summary>
        public Guid SeriesEpisodeId { get; set; }

        /// <summary>
        /// Bölüm numarası
        /// </summary>
        public int EpisodeNo { get; set; } = 1;

        /// <summary>
        /// bölüm başlığı
        /// </summary>
        public string? Title { get; set; } = "";

        /// <summary>
        /// bölüm türü
        /// </summary>
        public string SeriesEpisodeType { get; set; } = "";

        /// <summary>
        /// bölümün türünün id'si
        /// </summary>
        public int SeriesEpisodeTypesId { get; set; }

        /// <summary>
        /// bölümü ilk oluşturan
        /// </summary>
        public string CreateUser { get; set; } = "";

        /// <summary>
        /// bölümü son güncelleyen
        /// </summary>
        public string UpdateUser { get; set; } = "";

        /// <summary>
        /// bölümü editleyen
        /// </summary>
        public string EditorUser { get; set; } = "";

        /// <summary>
        /// bölümü çeviren
        /// </summary>
        public string TranslatorUser { get; set; } = "";

    }
}
