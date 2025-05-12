using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NutriPlat.Web.Services;
using NutriPlat.Web.Services.Interfaces;

namespace NutriPlat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSession(); // para guardar el JWT
            builder.Services.AddHttpClient(); // para consumir la API
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Auth/Login"; // Ruta para redirigir si no está autenticado
                        options.AccessDeniedPath = "/Auth/AccessDenied"; // Ruta si el rol no tiene permiso
                    });


            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260/"); // Ajusta al puerto real de tu API
            });


            builder.Services.AddAuthorization();




    
            builder.Services.AddHttpClient<ITrainerService, TrainerService>();


            var app = builder.Build();

            //  Manejo de errores y seguridad
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); // importante: antes de Authorization

            app.UseAuthorization();

            //  Rutas por defecto
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
