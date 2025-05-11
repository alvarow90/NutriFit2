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
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:7260"
                    : "https://localhost:7260";

            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<IProgressApiService, ProgressApiService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<IWorkoutRoutineApiService, WorkoutRoutineApiService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<INutritionPlanApiService, NutritionPlanApiService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<INutritionistService, NutritionistService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });



            // 👤 ViewModels y Views
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginView>();

            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<DashboardView>();

            builder.Services.AddSingleton<ProgressViewModel>();
            builder.Services.AddSingleton<ProgressView>();

            builder.Services.AddSingleton<WorkoutRoutineViewModel>();
            builder.Services.AddSingleton<WorkoutRoutineView>();

            builder.Services.AddSingleton<NutritionPlanView>();
            builder.Services.AddSingleton<NutritionPlanViewModel>();
            ;

            return builder.Build();
        }
    }
}
