namespace MangaFatihi.Models.Auth
{
    public class TokenModel
    {
        public string AccessToken { get; set; } = "";
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = "";
    }
}
