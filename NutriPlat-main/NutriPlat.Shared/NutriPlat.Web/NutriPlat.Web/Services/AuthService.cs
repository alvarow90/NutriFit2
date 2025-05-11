using NutriPlat.Shared.Dtos.Auth;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7260/api/");
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            Console.WriteLine("🔄 Enviando solicitud de login a la API...");

            try
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);

                Console.WriteLine("📡 Respuesta recibida");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Login exitoso desde web");
                    return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Error en login desde web:");
                Console.WriteLine($"Código: {response.StatusCode}");
                Console.WriteLine($"Respuesta: {errorContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 Excepción durante login:");
                Console.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
