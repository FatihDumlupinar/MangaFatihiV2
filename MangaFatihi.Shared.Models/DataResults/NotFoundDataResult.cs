namespace MangaFatihi.Shared.Models.DataResults
{
    /// <summary>
    /// Aranan kayıt bulunamadığında dönen data result
    /// </summary>
    public class NotFoundDataResult<T> : DataResult<T> where T : class, new()
    {
        public NotFoundDataResult(string message, string messageCode) : base(default, 404, message, messageCode)
        {

        }

    }
}
