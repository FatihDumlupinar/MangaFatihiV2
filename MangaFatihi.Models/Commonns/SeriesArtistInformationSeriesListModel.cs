namespace MangaFatihi.Models.Commonns
{
    public class SeriesArtistInformationSeriesListModel
    {
        /// <summary>
        /// Sanatçı ile ilişkilendirilen Serinin Unique Id'si
        /// </summary>
        public Guid SeriesId { get; set; }

        /// <summary>
        /// Sanatçı ile ilişkilendirilen Serinin başlığı
        /// </summary>
        public string SeriesTitle { get; set; } = "";
    }
}
