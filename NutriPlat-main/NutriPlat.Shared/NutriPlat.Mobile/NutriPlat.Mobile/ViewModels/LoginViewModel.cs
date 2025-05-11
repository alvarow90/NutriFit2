using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriPlat.Mobile.Services;
using NutriPlat.Shared.Dtos.Auth;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;


namespace NutriPlat.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool hasError = false;

        [RelayCommand]
        private async Task LoginAsync()
        {
            HasError = false;
            ErrorMessage = string.Empty;

            var loginDto = new LoginRequestDto
            {
                Email = Email,
                Password = Password
            };

            var tokenResponse = await _authService.LoginAsync(loginDto);

            if (tokenResponse != null)
            {
                // Guardar el token y datos en SecureStorage
                await SecureStorage.Default.SetAsync("access_token", tokenResponse.AccessToken);
                await SecureStorage.Default.SetAsync("user_id", tokenResponse.UserId);
                await SecureStorage.Default.SetAsync("user_role", tokenResponse.Role);
                await SecureStorage.Default.SetAsync("user_name", tokenResponse.FullName);

                // Navegar a la pantalla principal (ajusta según tu AppShell)
                await Shell.Current.GoToAsync("//Dashboard");
            }
            else
            {
                HasError = true;
                ErrorMessage = "Correo o contraseña incorrectos.";
            }
        }
    }
}
