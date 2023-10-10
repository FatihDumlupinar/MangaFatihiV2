namespace MangaFatihi.Shared.Models.API.Commons.Series
{
    public class SeriesListModel
    {
        /// <summary>
        /// Serinin unique id'si
        /// </summary>
        public Guid SeriesId { get; set; } = Guid.Empty;

        /// <summary>
        /// Serinin Başlığı
        /// </summary>
        public string SeriesTitle { get; set; } = "";

        /// <summary>
        /// Serinin kapak fotoğrafı
        /// </summary>
        public string? SeriesProfileImgUrl { get; set; } = "";

        /// <summary>
        /// Serinin ilk Yayın Tarihi
        /// </summary>
        public DateTime? SeriesBroadcastStartDate { get; set; }

        /// <summary>
        /// Siteye eklenme tarihi
        /// </summary>
        public DateTime? SeriesStartDateOnPage { get; set; }

        /// <summary>
        /// Serinin durum bilgisi
        /// </summary>
        public string SeriesStatus { get; set; } = "";

        /// <summary>
        /// Serinin durum bilgisinin id'si
        /// </summary>
        public int SeriesStatusId { get; set; }

        /// <summary>
        /// Serinin Türü
        /// </summary>
        public string SeriesType { get; set; } = "";

        /// <summary>
        /// Serinin Türünün id'si
        /// </summary>
        public int SeriesTypesId { get; set; }

        /// <summary>
        /// Seriyi ekleyen kullanıcı
        /// </summary>
        public string CreatedUserName { get; set; } = "";

        /// <summary>
        /// Seriyi son güncelleyen kullanıcı
        /// </summary>
        public string UpdatedUserName { get; set; } = "";

        /// <summary>
        /// Serinin sanatçıları
        /// </summary>
        public List<StandartModel> SeriesArtists { get; set; } = new();

        /// <summary>
        /// Serinin yazarları
        /// </summary>
        public List<StandartModel> SeriesAuthors { get; set; } = new();

        /// <summary>
        /// Serinin kategorileri
        /// </summary>
        public List<StandartModel> SeriesCategories { get; set; } = new();
    }
}
