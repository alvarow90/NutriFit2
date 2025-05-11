using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NutriPlat.Web.Services;

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

            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            //  Manejo de errores y seguridad
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

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
