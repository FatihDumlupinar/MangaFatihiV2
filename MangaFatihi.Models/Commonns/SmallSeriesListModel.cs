namespace MangaFatihi.Models.Commonns
{
    public class SmallSeriesListModel
    {
        /// <summary>
        /// Serinin Unique Id'si
        /// </summary>
        public Guid SeriesId { get; set; }

        /// <summary>
        /// Serinin başlığı
        /// </summary>
        public string SeriesTitle { get; set; } = "";
    }
}
