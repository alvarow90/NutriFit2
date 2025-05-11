using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace NutriPlat.Mobile.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string fullName = string.Empty;

        [ObservableProperty]
        private string role = string.Empty;

        public DashboardViewModel()
        {
            LoadUserData();
        }

        private async void LoadUserData()
        {
            FullName = await SecureStorage.Default.GetAsync("user_name") ?? "Usuario";
            Role = await SecureStorage.Default.GetAsync("user_role") ?? "Desconocido";
        }

        [RelayCommand]
        private async Task IrAProgreso()
        {
            await Shell.Current.GoToAsync("//Progress");
        }

        [RelayCommand]
        private async Task IrARutina()
        {
            await Shell.Current.GoToAsync("//WorkoutRoutine");
        }

        [RelayCommand]
        private async Task IrANutricion()
        {
            await Shell.Current.GoToAsync("//NutritionPlan");
        }


        [RelayCommand]
        private async Task CerrarSesionAsync()
        {
            try
            {
                SecureStorage.Default.RemoveAll();
                await Shell.Current.GoToAsync("//Login");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cerrar sesión: {ex.Message}", "OK");
            }
        }



    }
}
