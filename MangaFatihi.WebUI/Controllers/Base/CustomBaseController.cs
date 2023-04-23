using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Controllers.Base
{
    /// <summary>
    /// Bütün Controller larda ortak olan özellikleri barındıran Base Controller
    /// </summary>
    public class CustomBaseController<T> : Controller
    {
        // Note : Eğer Contructor kullansaydım, bu base sınıfında türeyen diğer controllerda da Contructor yazmak zorundaydım.

        private ILogger<T> _logger;

        /// <summary>
        /// Controller katmanında loglama için kullanılan Logger
        /// </summary>
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

    }
}
