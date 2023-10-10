namespace MangaFatihi.Shared.Models.DataResults
{
    /// <summary>
    /// Hata döndürmek için kullanılır
    /// </summary>
    public class ErrorDataResult<T> : DataResult<T> where T : class, new()
    {
        public ErrorDataResult(T data, string message, string messageCode) : base(data, 400, message, messageCode)
        {

        }

        public ErrorDataResult(string message, string messageCode) : base(default, 400, message, messageCode)
        {

        }

    }
}
