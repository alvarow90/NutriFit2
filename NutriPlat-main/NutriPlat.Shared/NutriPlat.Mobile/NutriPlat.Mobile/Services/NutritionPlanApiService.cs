using NutriPlat.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace NutriPlat.Mobile.Services
{
    public class NutritionPlanApiService : INutritionPlanApiService
    {
        private readonly HttpClient _httpClient;

        public NutritionPlanApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<UserNutritionPlanDto?> GetMyNutritionPlanAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/nutritionplans/my");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserNutritionPlanDto>();

            return null;
        }
    }
}
