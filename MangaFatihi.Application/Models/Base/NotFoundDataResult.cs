namespace MangaFatihi.Application.Models.Base
{
    public class NotFoundDataResult<T> : DataResult<T> where T : class, new()
    {
        public NotFoundDataResult(string message, string messageCode) : base(default, 404, message, messageCode)
        {

        }

    }
}
