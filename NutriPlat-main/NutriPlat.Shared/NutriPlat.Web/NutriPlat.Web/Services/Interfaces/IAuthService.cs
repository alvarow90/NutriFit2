using NutriPlat.Shared.Dtos.Auth;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginRequestDto loginDto);

    }
}
