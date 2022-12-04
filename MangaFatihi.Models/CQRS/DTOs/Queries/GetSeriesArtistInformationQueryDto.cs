﻿using MangaFatihi.Models.Commonns;

namespace MangaFatihi.Models.CQRS.DTOs.Queries
{
    public class GetSeriesArtistInformationQueryDto
    {
        /// <summary>
        /// Sanatçının Unique Id'si
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Sanatçının adı soyadı
        /// </summary>
        public string FullName { get; set; } = "";

        /// <summary>
        /// Sanatçı ilişkilendirilen serilerin listesi
        /// </summary>
        public List<SeriesArtistInformationSeriesListModel> SeriesList { get; set; } = new();

    }
}
