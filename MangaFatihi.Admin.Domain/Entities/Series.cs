using MangaFatihi.Admin.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class Series : BaseEntity
    {
        /// <summary>
        /// Serinin Başlığı
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Serinin alternatif veya farklı dillerdeki başlıkları
        /// </summary>
        public string? TitleAlternative { get; set; }

        /// <summary>
        /// Serinin özet hikayesi
        /// </summary>
        public string? Story { get; set; }

        /// <summary>
        /// Serinin kapak fotoğrafı
        /// </summary>
        public string? ProfileImgUrl { get; set; }

        /// <summary>
        /// Seri ile ilgili not
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Serinin ilk Yayın Tarihi
        /// </summary>
        public DateTime? BroadcastStartDate { get; set; }

        /// <summary>
        /// Siteye eklenme tarihi
        /// </summary>
        public DateTime? StartDateOnPage { get; set; }

        /// <summary>
        /// Güncel mi
        /// </summary>
        public bool IsUpToDate { get; set; } = false;

        /// <summary>
        /// Anasayfadaki Slyder da olacak mı 
        /// </summary>
        public bool IsSlyder { get; set; } = false;

        /// <summary>
        /// Yeni mi
        /// </summary>
        public bool IsNew { get; set; } = false;

        /// <summary>
        /// Serinin durum bilgisi
        /// </summary>
        public virtual StaticSeriesStatus StaticSeriesStatus { get; set; }
        public int StaticSeriesStatusId { get; set; }

        /// <summary>
        /// Serinin Türü
        /// </summary>
        public virtual StaticSeriesType StaticSeriesTypes { get; set; }
        public int StaticSeriesTypesId { get; set; }

        public virtual IList<SeriesAndSeriesArtist> SeriesAndSeriesArtists { get; set; }
        public virtual IList<SeriesAndSeriesAuthor> SeriesAndSeriesAuthor { get; set; }
        public virtual IList<SeriesAndSeriesCategory> SeriesAndSeriesCategories { get; set; }

        public virtual IList<SeriesEpisode> SeriesEpisodes { get; set; }


    }
}
