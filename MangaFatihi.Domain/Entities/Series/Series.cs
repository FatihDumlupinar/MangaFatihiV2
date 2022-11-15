using MangaFatihi.Domain.Common;

namespace MangaFatihi.Domain.Entities
{
    public class Series : BaseEntity
    {
        public string Title { get; set; }

        public string? TitleAlternative { get; set; }

        public string? Story { get; set; }

        public string ProfileImgUrl { get; set; }

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

        //Bire çok ilişkiler
        public virtual StaticSeriesStatus StaticSeriesStatus { get; set; }
        public virtual StaticSeriesType StaticSeriesTypes { get; set; }

        //Çoka çok ilişkiler
        public virtual IList<SeriesArtist> SeriesArtists { get; set; }
        public virtual IList<SeriesAuthor> SeriesAuthors { get; set; }
        public virtual IList<SeriesCategory> SeriesCategories { get; set; }
        public virtual IList<SeriesEpisode> SeriesEpisodes { get; set; }


    }
}
