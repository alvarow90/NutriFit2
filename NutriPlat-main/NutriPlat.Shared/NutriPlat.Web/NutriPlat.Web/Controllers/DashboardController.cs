using Microsoft.AspNetCore.Mvc;

namespace NutriPlat.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("FullName") ?? "Usuario";
            ViewBag.Role = HttpContext.Session.GetString("UserRole") ?? "Sin rol";

            return View();
        }
    }
}
