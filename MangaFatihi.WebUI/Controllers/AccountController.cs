using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MangaFatihi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public async ValueTask<IActionResult> LoginAsync(string returnUrl = "/")
        {
            // "Google" değeri, Startup.cs dosyasında tanımlanan kimlik sağlayıcı adıyla eşleşmelidir.
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Google");
        }

        public async ValueTask<IActionResult> Logout()
        {
            // Kullanıcının oturumunu kapatır.
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            return RedirectToAction("Index", "Home");
        }
    }
}
