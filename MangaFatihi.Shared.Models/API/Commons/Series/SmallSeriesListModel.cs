namespace MangaFatihi.Shared.Models.API.Commons.Series
{
    public class SmallSeriesListModel
    {
        /// <summary>
        /// Serinin Unique Id'si
        /// </summary>
        public Guid SeriesId { get; set; } = Guid.Empty;

        /// <summary>
        /// Serinin başlığı
        /// </summary>
        public string SeriesTitle { get; set; } = "";
    }
}
