using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using NutriPlat.Mobile.Services;
using NutriPlat.Shared.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.ViewModels
{
    public partial class WorkoutRoutineViewModel : ObservableObject
    {
        private readonly IWorkoutRoutineApiService _workoutService;

        [ObservableProperty]
        private ObservableCollection<UserWorkoutRoutineDto> assignedRoutines = new();

        [ObservableProperty]
        private UserWorkoutRoutineDto? routine;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private string message = string.Empty;

        public WorkoutRoutineViewModel(IWorkoutRoutineApiService workoutService)
        {
            _workoutService = workoutService;
            LoadWorkoutRoutinesCommand.Execute(null); // <- correcto nombre del comando
        }

        [RelayCommand]
        private async Task LoadWorkoutRoutinesAsync()
        {
            try
            {
                IsLoading = true;
                var rutina = await _workoutService.GetMyWorkoutRoutineAsync();
                if (rutina != null)
                    AssignedRoutines = new ObservableCollection<UserWorkoutRoutineDto> { rutina };
                else
                    Message = "No tienes una rutina asignada aún.";
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Shell.Current.DisplayAlert("Sesión expirada", "Por favor inicia sesión de nuevo.", "Aceptar");
                SecureStorage.Default.RemoveAll();
                await Shell.Current.GoToAsync("//Login");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar las rutinas: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
