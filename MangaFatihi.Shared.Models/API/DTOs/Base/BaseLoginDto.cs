namespace MangaFatihi.Shared.Models.API.DTOs.CQRS.Base
{
    public class BaseLoginDto
    {
        public string AccessToken { get; set; } = "";
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = "";
    }
}
