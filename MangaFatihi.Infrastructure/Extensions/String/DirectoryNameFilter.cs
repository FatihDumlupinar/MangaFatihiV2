using System.Text.RegularExpressions;

namespace MangaFatihi.Infrastructure.Extensions.String
{
    public static class DirectoryNameFilter
    {
        public static string CleanDirectoryName(this string directoryName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();

            var validFileName = new string(directoryName.Where(x => !invalidChars.Contains(x)).ToArray());

            var result = Regex.Replace(validFileName, @"\s+", "_");

            return result;
        }
    }
}
