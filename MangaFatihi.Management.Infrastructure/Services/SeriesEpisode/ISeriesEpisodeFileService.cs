using Microsoft.AspNetCore.Http;

namespace MangaFatihi.Management.Infrastructure.Services.SeriesEpisode
{
    public interface ISeriesEpisodeFileService
    {
        public Task<string> UploadSeriesEpisodeImageAsync(IFormFile file, string seriesName, int seriesEpisodeNo, CancellationToken cancellationToken = default);

        public Task<List<string>> UploadMultiSeriesEpisodeIImagesAsync(List<IFormFile> files, string seriesName, int seriesEpisodeNo, CancellationToken cancellationToken = default);
    }
}
