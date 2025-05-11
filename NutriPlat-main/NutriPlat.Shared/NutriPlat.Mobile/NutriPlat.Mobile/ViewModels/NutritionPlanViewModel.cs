using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriPlat.Mobile.Services;
using NutriPlat.Shared.Dtos;


namespace NutriPlat.Mobile.ViewModels
{
    public partial class NutritionPlanViewModel : ObservableObject
    {
        private readonly INutritionPlanApiService _nutritionService;

        public NutritionPlanViewModel(INutritionPlanApiService nutritionService)
        {
            _nutritionService = nutritionService;
            LoadPlanCommand.Execute(null);
        }

        [ObservableProperty]
        private UserNutritionPlanDto? plan;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private string message = string.Empty;

        [RelayCommand]
        private async Task LoadPlanAsync()
        {
            try
            {
                IsLoading = true;
                Plan = await _nutritionService.GetMyNutritionPlanAsync();
                Message = Plan == null ? "No tienes un plan de nutrición asignado." : string.Empty;
            }
            catch (Exception ex)
            {
                Message = $"Error al cargar el plan: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }



        private readonly INutritionistService _nutritionistService;

        public NutritionPlanViewModel(INutritionPlanApiService nutritionService, INutritionistService nutritionistService)
        {
            _nutritionService = nutritionService;
            _nutritionistService = nutritionistService;
            LoadDataCommand.Execute(null);
        }

        [ObservableProperty]
        private UserDto? nutritionist;

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                Plan = await _nutritionService.GetMyNutritionPlanAsync();
                Nutritionist = await _nutritionistService.GetMyNutritionistAsync();
            }
            catch (Exception ex)
            {
                Message = $"Error al cargar datos: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
