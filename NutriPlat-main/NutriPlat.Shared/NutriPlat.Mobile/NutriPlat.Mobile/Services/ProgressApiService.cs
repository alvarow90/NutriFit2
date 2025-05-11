using NutriPlat.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace NutriPlat.Mobile.Services
{
    public class ProgressApiService : IProgressApiService
    {
        private readonly HttpClient _httpClient;


      
        public ProgressApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");

            System.Diagnostics.Debug.WriteLine($"🔑 Token usado: {token}");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IEnumerable<ProgressEntryDto>> GetMyProgressEntriesAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/progresstracking/my");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProgressEntryDto>>() ?? [];

            return [];
        }

        public async Task<bool> CreateProgressEntryAsync(ProgressEntryDto entry)
        {
            await SetAuthorizationHeaderAsync();

            // 🔍 Imprimir el objeto que estás enviando
            var jsonBody = System.Text.Json.JsonSerializer.Serialize(entry);
            System.Diagnostics.Debug.WriteLine($"📤 Enviando progreso: {jsonBody}");

            var response = await _httpClient.PostAsJsonAsync("api/progresstracking", entry);

            // 📥 Imprimir el status y el contenido de la respuesta
            var responseContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"📥 Código de respuesta: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"📥 Respuesta completa: {responseContent}");

            return response.IsSuccessStatusCode;
        }




    }
}
