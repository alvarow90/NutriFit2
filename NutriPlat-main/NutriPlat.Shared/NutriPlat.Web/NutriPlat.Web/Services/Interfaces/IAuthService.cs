using NutriPlat.Shared.Dtos.Auth;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto);
        Task<bool> RegisterAsync(RegisterRequestDto dto);
    }
}
