using MangaFatihi.Application.Models.DTOs.Helpers;

namespace MangaFatihi.Application.Models.DTOs.Queries
{
    public class GetSeriesInformationQueryDto
    {
        /// <summary>
        /// Serinin Başlığı
        /// </summary>
        public string SeriesTitle { get; set; } = "";

        /// <summary>
        /// Serinin alternatif veya farklı dillerdeki başlıkları
        /// </summary>
        public string? SeriesTitleAlternative { get; set; } = "";

        /// <summary>
        /// Serinin özet hikayesi
        /// </summary>
        public string? SeriesStory { get; set; } = "";

        /// <summary>
        /// Serinin kapak fotoğrafı
        /// </summary>
        public string? SeriesProfileImgUrl { get; set; } = "";

        /// <summary>
        /// Seri ile ilgili not
        /// </summary>
        public string? SeriesNote { get; set; } = "";

        /// <summary>
        /// Serinin ilk Yayın Tarihi
        /// </summary>
        public DateTime? SeriesBroadcastStartDate { get; set; }

        /// <summary>
        /// Siteye eklenme tarihi
        /// </summary>
        public DateTime? SeriesStartDateOnPage { get; set; }

        /// <summary>
        /// Güncel mi
        /// </summary>
        public bool SeriesIsUpToDate { get; set; } = false;

        /// <summary>
        /// Anasayfadaki Slyder da olacak mı 
        /// </summary>
        public bool SeriesIsSlyder { get; set; } = false;

        /// <summary>
        /// Yeni mi
        /// </summary>
        public bool SeriesIsNew { get; set; } = false;

        /// <summary>
        /// Serinin durum bilgisi
        /// </summary>
        public string SeriesStatus { get; set; } = "";

        /// <summary>
        /// Serinin durum bilgisi
        /// </summary>
        public int SeriesStatusId { get; set; }

        /// <summary>
        /// Serinin Türü
        /// </summary>
        public string SeriesTypes { get; set; } = "";

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
