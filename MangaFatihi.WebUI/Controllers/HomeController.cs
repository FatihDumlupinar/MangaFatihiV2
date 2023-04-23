using MangaFatihi.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Controllers
{
    public class HomeController : CustomBaseController<HomeController>
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}