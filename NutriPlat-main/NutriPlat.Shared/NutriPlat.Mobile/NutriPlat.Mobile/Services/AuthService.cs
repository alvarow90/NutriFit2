using NutriPlat.Shared.Dtos.Auth;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            return null;
        }
    }
}
