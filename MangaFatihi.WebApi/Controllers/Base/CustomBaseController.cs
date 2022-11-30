using MangaFatihi.Models.Base;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebApi.Controllers.Base
{
    /// <summary>
    /// Bütün Controller larda ortak olan özellikleri barındıran Base Controller
    /// </summary>
    /// <typeparam name="T">Statik olmayan, new'lenebilen sınıf olmak zorunda</typeparam>
    [ApiController]
    public class CustomBaseController<T> : ControllerBase 
        where T : class, new()
    {
        // Note : Eğer Contructor kullansaydım, bu base sınıfında türeyen diğer controllerda da Contructor yazmak zorundaydım. Ee ben bunu yazacaksam ne anladık base sınıfa.

        #region Fields

        private IMediator _mediator;

        private ILogger<T> _logger;


        #endregion

        #region Properties

        /// <summary>
        /// CQRS için kullanılan Mediator 
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Controller katmanında loglama için kullanılan Logger
        /// </summary>
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

        #endregion

        /// <summary>
        /// Standart geri dönüş modeli döndüren fonksiyon
        /// </summary>
        [NonAction]
        public IActionResult CustomStandartReturnAction<TData>(DataResult<TData> dataResult) where TData : class, new()
        {
            return new ObjectResult(dataResult)
            { StatusCode = dataResult.StatusCode };
        }

    }
}
