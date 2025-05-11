// NutriPlat.Mobile/Services/IAuthService.cs
using NutriPlat.Shared.Dtos.Auth;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto);
    }
}
