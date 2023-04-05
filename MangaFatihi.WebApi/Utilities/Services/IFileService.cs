namespace MangaFatihi.WebApi.Utilities.Services
{
    /// <summary>
    /// Dosya işlemleri servisi
    /// </summary>
    public interface IFileService : IDisposable
    {
        /// <summary>
        /// Tekli dosya yükleme
        /// </summary>
        /// <returns>yüklenen dosya adresi</returns>
        public Task<string> UploadSeriesImageAsync(IFormFile file, CancellationToken cancellationToken = default);

        /// <summary>
        /// Çoklu dosya yükleme
        /// </summary>
        /// <returns>yüklenen dosyaların adresleri</returns>
        public Task<List<string>> UploadMultiSeriesImagesAsync(List<IFormFile> files, CancellationToken cancellationToken = default);
    }
}
