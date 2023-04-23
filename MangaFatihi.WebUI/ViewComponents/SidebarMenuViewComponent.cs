using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.ViewComponents
{
    [ViewComponent(Name = "SidebarMenu")]
    public class SidebarMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
