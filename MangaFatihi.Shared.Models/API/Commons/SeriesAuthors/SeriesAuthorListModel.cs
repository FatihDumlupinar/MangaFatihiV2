﻿namespace MangaFatihi.Shared.Models.API.Commons.SeriesAuthors
{
    public class SeriesAuthorListModel
    {
        /// <summary>
        /// Yazarın Unique Id'si
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Yazarın Adı Soyadı
        /// </summary>
        public string FullName { get; set; } = "";

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Oluşturanın Adı Soyadı
        /// </summary>
        public string? CreateUser { get; set; } = "";

        /// <summary>
        /// Son güncellenme tarihi
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// Son güncelleyenin adı soyadı
        /// </summary>
        public string? UpdateUser { get; set; } = "";
    }
}