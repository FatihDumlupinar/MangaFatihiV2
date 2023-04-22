using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
