using Microsoft.AspNetCore.Mvc;



namespace KuaforYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        // Ana sayfa
        public IActionResult Index()
        {
            return View();
        }

        // Hakkında sayfası (isteğe bağlı)
        public IActionResult About()
        {
            return View();
        }
    }
}
