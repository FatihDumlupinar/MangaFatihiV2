namespace MangaFatihi.Infrastructure.Providers
{
    public interface IStaticFileDirectoryProvider
    {
        /// <summary>
        /// Api katmanındaki wwwroot klasörün yolunu getiriyor
        /// </summary>
        string GetStaticFileDirectory();
    }
}
