﻿namespace MangaFatihi.Models.Commonns
{
    public class SeriesCategoryListModel
    {
        /// <summary>
        /// Sanatçının Unique Id'si
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Kategorisinin Adı
        /// </summary>
        public string Name { get; set; } = "";

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
