using MangaFatihi.Domain.Constants;
using MangaFatihi.Infrastructure.Extensions.String;
using MangaFatihi.Infrastructure.Providers;
using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Infrastructure.Services.SeriesEpisode
{
    public class SeriesEpisodeFileService : ISeriesEpisodeFileService
    {
        private readonly IStaticFileDirectoryProvider _staticFileDirectoryProvider;
        private readonly string _CurrentDirectorySeriesPath;

        public SeriesEpisodeFileService(IStaticFileDirectoryProvider staticFileDirectoryProvider)
        {
            _staticFileDirectoryProvider = staticFileDirectoryProvider;

            //wwwroot/series 
            _CurrentDirectorySeriesPath = Path.Combine(_staticFileDirectoryProvider.GetStaticFileDirectory(), ApplicationStaticStrings.CURRENT_SERIES_DIRECTORY_NAME);

            //eğer CurrentDirectory değerindeki dosya yoksa oluştur
            if (!Directory.Exists(_CurrentDirectorySeriesPath))
            {
                Directory.CreateDirectory(_CurrentDirectorySeriesPath);
            }
        }

        public async Task<List<string>> UploadMultiSeriesEpisodeIImagesAsync(List<IFormFile> files, string seriesName, int seriesEpisodeNo, CancellationToken cancellationToken = default)
        {
            //../wwwroot/series/{seriesName}
            var directoryPath = Path.Combine(_CurrentDirectorySeriesPath, seriesName.CleanDirectoryName());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            //../wwwroot/series/{seriesName}/{seriesEpisodeNo}
            directoryPath = Path.Combine(directoryPath, seriesEpisodeNo.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var uploadTasks = files.Select(async file =>
            {
                var filename = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(directoryPath, filename);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                return Path.Combine(ApplicationStaticStrings.CURRENT_SERIES_DIRECTORY_NAME, seriesName, seriesEpisodeNo.ToString(), filename);
            });

            return (await Task.WhenAll(uploadTasks)).ToList();
        }

        public async Task<string> UploadSeriesEpisodeImageAsync(IFormFile file, string seriesName, int seriesEpisodeNo, CancellationToken cancellationToken = default)
        {
            //../wwwroot/series/{seriesName}
            var filePath = Path.Combine(_CurrentDirectorySeriesPath, seriesName.CleanDirectoryName());
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //../wwwroot/series/{seriesName}/{seriesEpisodeNo}
            filePath = Path.Combine(filePath, seriesEpisodeNo.ToString());
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string filename = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";

            filePath = Path.Combine(filePath, filename);

            await using Stream stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            //geriye kısa adresi dönüyoruz => /series/{seriesName}/{seriesEpisodeNo}
            var currentDirectoryAndFileName = Path.Combine(ApplicationStaticStrings.CURRENT_SERIES_DIRECTORY_NAME, seriesName, seriesEpisodeNo.ToString(), filename);

            return currentDirectoryAndFileName;
        }
    }
}
