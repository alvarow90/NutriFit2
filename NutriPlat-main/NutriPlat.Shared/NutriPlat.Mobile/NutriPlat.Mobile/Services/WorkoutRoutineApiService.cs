using NutriPlat.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace NutriPlat.Mobile.Services
{
    public class WorkoutRoutineApiService : IWorkoutRoutineApiService
    {
        private readonly HttpClient _httpClient;

        public WorkoutRoutineApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.Default.GetAsync("access_token");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Obtiene la rutina actual del usuario autenticado (cliente).
        /// </summary>
        public async Task<UserWorkoutRoutineDto?> GetMyWorkoutRoutineAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/routines/my");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserWorkoutRoutineDto>();
            }

            return null;
        }

        /// <summary>
        /// Obtiene todas las rutinas asignadas al cliente autenticado.
        /// </summary>
        public async Task<IEnumerable<UserWorkoutRoutineDto>> GetMyAssignedRoutinesAsync()
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync("api/routines/my/all");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserWorkoutRoutineDto>>() ?? [];
            }

            return [];
        }
    }
}
