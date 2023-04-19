using MangaFatihi.Infrastructure.Providers;

namespace MangaFatihi.WebApi.Utilities.Providers
{
    public class StaticFileDirectoryProvider : IStaticFileDirectoryProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StaticFileDirectoryProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetStaticFileDirectory()
        {
            return _webHostEnvironment.WebRootPath;
        }
    }

}
