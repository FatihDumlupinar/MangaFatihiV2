namespace MangaFatihi.Application.Models.Base
{
    public class DataResult<T> where T : class, new()
    {
        //Swagger da gösterebilmek için Generik, object olmaz
        public T? Data { get; }

        public int StatusCode { get; }

        public string Message { get; }

        public string MessageCode { get; }

        public DataResult(T? data, int statusCode, string message, string messageCode)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            MessageCode = messageCode;
        }
    }
}
