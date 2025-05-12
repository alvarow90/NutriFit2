// NutriPlat.Web/Services/TrainerService.cs
using NutriPlat.Shared.Dtos;
using NutriPlat.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly HttpClient _httpClient;

        public TrainerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7260/api/");
        }

        public async Task<bool> CreateRoutineAsync(WorkoutRoutineDto routine, string jwtToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "workoutroutines");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                request.Content = JsonContent.Create(routine);

                var response = await _httpClient.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Error al crear rutina: {ex.Message}");
                return false;
            }
        }
    }
}
