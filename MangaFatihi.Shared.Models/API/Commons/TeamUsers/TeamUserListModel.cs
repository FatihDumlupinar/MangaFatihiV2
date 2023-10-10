namespace MangaFatihi.Shared.Models.API.Commons.TeamUsers
{
    public class TeamUserListModel
    {
        /// <summary>
        /// Kullanıcının Uniqe Id'si
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Kullanıcının Adı ve Soyad
        /// </summary>
        public string? FullName { get; set; } = "";

    }
}
