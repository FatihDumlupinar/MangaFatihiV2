namespace MangaFatihi.Models.Commonns
{
    public class SeriesArtistListModel
    {
        /// <summary>
        /// Sanatçının Adı Soyadı
        /// </summary>
        public string FullName { get; set; } = "";

        /// <summary>
        /// Sanatçının Unique Id'si
        /// </summary>
        public Guid Id { get; set; }

    }
}
