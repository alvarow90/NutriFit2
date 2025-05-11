using NutriPlat.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace NutriPlat.Mobile.Services
{
    public class NutritionistService : INutritionistService
    {
        private readonly HttpClient _httpClient;

        public NutritionistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            System.Diagnostics.Debug.WriteLine($"📡 BaseAddress: {_httpClient.BaseAddress}");
        }

        private async Task SetAuthHeader()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<UserDto?> GetMyNutritionistAsync()
        {
            await SetAuthHeader();

            var response = await _httpClient.GetAsync("api/users/me/nutritionist");

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<UserDto>()
                : null;
        }
    }
}
