using NutriPlat.Shared.Dtos.Auth;
using NutriPlat.Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
