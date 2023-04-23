using MangaFatihi.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Controllers
{
    public class AdminController : CustomBaseController<AdminController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
