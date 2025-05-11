using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
