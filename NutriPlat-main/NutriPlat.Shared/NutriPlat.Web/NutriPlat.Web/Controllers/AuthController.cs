using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriPlat.Shared.Dtos.Auth;
using NutriPlat.Shared.Enums;
using NutriPlat.Web.Models;
using NutriPlat.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var tokenResult = await _authService.LoginAsync(new LoginRequestDto
            {
                Email = model.Email,
                Password = model.Password
            });

            if (tokenResult == null)
            {
                model.ErrorMessage = "Credenciales incorrectas.";
                return View(model);
            }

            // Decodificar el token para extraer los claims (incluyendo rol)
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenResult.AccessToken);

            var claims = jwtToken.Claims.ToList();

            // Crear identidad y principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Guardar el token en la sesión (opcional)
            HttpContext.Session.SetString("AuthToken", tokenResult.AccessToken);

            // Autenticación vía cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = tokenResult.ExpiresAt
                });

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }



        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterAsync(new RegisterRequestDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Role = Enum.Parse<UserRole>(model.Role)

            });

            if (result)
            {
                model = new RegisterViewModel { SuccessMessage = "✅ Usuario registrado exitosamente." };
                return View(model);
            }

            model.ErrorMessage = "❌ Ocurrió un error al registrar el usuario.";
            return View(model);
        }

    }
}
