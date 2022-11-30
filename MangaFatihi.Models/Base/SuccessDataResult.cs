namespace MangaFatihi.Models.Base
{
    public class SuccessDataResult<T> : DataResult<T> where T : class, new()
    {
        public SuccessDataResult(T data, string message, string messageCode) : base(data, 200, message, messageCode)
        {

        }

        public SuccessDataResult(string message, string messageCode) : base(default, 200, message, messageCode)
        {

        }
    }
}
