using Microsoft.Extensions.Configuration;

namespace MangaFatihi.Shared.Appsettings
{
    public static class SharedAppsettingsConfig
    {
        /// <summary>
        /// Ortak appsettings.json dosyasını okumak için kullanılır.
        /// </summary>
        public static IConfigurationRoot AddSharedAppsettingsConfig(string? env = null)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //eğer buradaki json file ı bulursa yukarıdakini okumaz, yoksa yukarıdakine bakar
            .AddJsonFile($"appsettings.{env}.json", optional: true);

            return configuration.Build();
        }
    }
}
