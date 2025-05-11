using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriPlat.Mobile.Services;
using NutriPlat.Shared.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using NutriPlat.Shared.Dtos;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;


namespace NutriPlat.Mobile.ViewModels
{
    public partial class ProgressViewModel : ObservableObject
    {
        private readonly IProgressApiService _progressApi;

        public ProgressViewModel(IProgressApiService progressApi)
        {
            _progressApi = progressApi;
            LoadEntriesCommand.Execute(null);
        }

        // Lista de entradas del usuario
        [ObservableProperty]
        private ObservableCollection<ProgressEntryDto> progressEntries = new();

        // Datos para nueva entrada
        [ObservableProperty] private DateTime entryDate = DateTime.Now;
        [ObservableProperty] private decimal? weightKg;
        [ObservableProperty] private decimal? bodyFatPercentage;
        [ObservableProperty] private string notes = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        [RelayCommand]
        private async Task LoadEntriesAsync()
        {
            try
            {
                IsLoading = true;
                var entries = await _progressApi.GetMyProgressEntriesAsync();
                ProgressEntries = new ObservableCollection<ProgressEntryDto>(entries);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                // 🔒 Token inválido o expirado
                await Shell.Current.DisplayAlert("Sesión expirada", "Tu sesión ha expirado. Por favor inicia sesión de nuevo.", "Aceptar");
                SecureStorage.Default.RemoveAll();
                await Shell.Current.GoToAsync("//Login");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar progreso: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task SaveEntryAsync()
        {
            var newEntry = new ProgressEntryDto
            {
                EntryDate = EntryDate,
                WeightKg = WeightKg,
                BodyFatPercentage = BodyFatPercentage,
                Notes = Notes
            };

            try
            {
                var success = await _progressApi.CreateProgressEntryAsync(newEntry);
                if (success)
                {
                    Message = "Entrada registrada.";
                    await LoadEntriesAsync();
                    WeightKg = null;
                    BodyFatPercentage = null;
                    Notes = string.Empty;
                }
                else
                {
                    Message = "Error al registrar entrada.";
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                 SecureStorage.Default.RemoveAll();
                await Shell.Current.GoToAsync("//Login");
            }
        }


    }


}
