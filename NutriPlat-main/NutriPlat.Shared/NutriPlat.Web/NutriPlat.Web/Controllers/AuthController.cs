using Microsoft.AspNetCore.Mvc;
using NutriPlat.Shared.Dtos.Auth;
using NutriPlat.Web.Models;
using NutriPlat.Web.Services;

namespace NutriPlat.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine("🔄 Controller recibiendo solicitud de login...");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠️ Modelo de login inválido.");
                return View(model);
            }

            var loginDto = new LoginRequestDto
            {
                Email = model.Email,
                Password = model.Password
            };

            var tokenResponse = await _authService.LoginAsync(loginDto);

            if (tokenResponse != null)
            {
                Console.WriteLine("🔑 Token recibido exitosamente en el controller.");
                HttpContext.Session.SetString("AuthToken", tokenResponse.AccessToken);
                return RedirectToAction("Index", "Dashboard");
            }

            Console.WriteLine("❌ Falló la autenticación en el controller.");
            ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
