namespace MangaFatihi.WebApi.Utilities.Services
{
    /// <summary>
    /// Dosya işlemleri servisi
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Bölümleri yükleyeceğimiz ana dosyanın adı
        /// </summary>
        private readonly static string CurrentSeriesDirectory = "series";

        /// <summary>
        /// Resimler tam dosya yolu
        /// </summary>
        private readonly static string CurrentDirectorySeriesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", CurrentSeriesDirectory);

        public FileService()
        {
            //eğer CurrentDirectory değerindeki dosya yoksa oluştur
            if (!Directory.Exists(CurrentDirectorySeriesPath))
            {
                Directory.CreateDirectory(CurrentDirectorySeriesPath);
            }
        }

        /// <summary>
        /// Tekli dosya yükleme
        /// </summary>
        /// <returns>yüklenen dosya adresi</returns>
        public async Task<string> UploadSeriesImageAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var filename = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(CurrentDirectorySeriesPath, filename);

            await using Stream stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            //geriye kısa adresi dönüyoruz
            var currentDirectoryAndFileName = Path.Combine(CurrentSeriesDirectory, filename);

            return currentDirectoryAndFileName;
        }

        /// <summary>
        /// Çoklu dosya yükleme
        /// </summary>
        /// <returns>yüklenen dosyaların adresleri</returns>
        public async Task<List<string>> UploadMultiSeriesImagesAsync(List<IFormFile> files, CancellationToken cancellationToken = default)
        {
            var result = new List<string>(capacity: files.Count);

            foreach (var file in files)
            {
                var filename = $"{Guid.NewGuid()}.{Path.GetExtension(file.FileName)}";

                var path = Path.Combine(CurrentDirectorySeriesPath, filename);

                await using Stream stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream, cancellationToken);

                //geriye kısa adresi dönüyoruz
                var currentDirectoryAndFileName = Path.Combine(CurrentSeriesDirectory, filename);

                result.Add(currentDirectoryAndFileName);
            }

            return result;
        }

        public void Dispose()
        {
        }
    }
}
