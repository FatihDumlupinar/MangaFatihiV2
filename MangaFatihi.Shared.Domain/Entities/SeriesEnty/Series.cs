﻿using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Entities.SeriesArtists;
using MangaFatihi.Shared.Domain.Entities.SeriesAuthors;
using MangaFatihi.Shared.Domain.Entities.SeriesCategories;
using MangaFatihi.Shared.Domain.Entities.SeriesEpisodes;
using MangaFatihi.Shared.Domain.Entities.Statics;
using MangaFatihi.Shared.Models.Enms;

namespace MangaFatihi.Shared.Domain.Entities.SeriesEnty
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
        public StaticSeriesStatusEnm StaticSeriesStatusId { get; set; }

        /// <summary>
        /// Serinin Türü
        /// </summary>
        public virtual StaticSeriesType StaticSeriesTypes { get; set; }
        public StaticSeriesTypeEnm StaticSeriesTypesId { get; set; }

        public virtual IList<SeriesAndSeriesArtist> SeriesAndSeriesArtists { get; set; }
        public virtual IList<SeriesAndSeriesAuthor> SeriesAndSeriesAuthor { get; set; }
        public virtual IList<SeriesAndSeriesCategory> SeriesAndSeriesCategories { get; set; }

        public virtual IList<SeriesEpisode> SeriesEpisodes { get; set; }


    }
}
