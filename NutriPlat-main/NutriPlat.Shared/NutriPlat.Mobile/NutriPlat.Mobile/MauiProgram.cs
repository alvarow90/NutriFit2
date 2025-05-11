using Microsoft.Extensions.Logging;
using NutriPlat.Mobile.Services;         // ← para IAuthService, AuthService
using NutriPlat.Mobile.ViewModels;       // ← para LoginViewModel
using NutriPlat.Mobile.Views;            // ← para LoginView
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection; // <-- ¡Este es el importante!

namespace NutriPlat.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // 🔐 HttpClient apuntando a la API
            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260"); // <-- Cambia el puerto si tu API usa otro
            });

            // 👤 ViewModels y Views
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginView>();

            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<DashboardView>();


            return builder.Build();
        }
    }
}
