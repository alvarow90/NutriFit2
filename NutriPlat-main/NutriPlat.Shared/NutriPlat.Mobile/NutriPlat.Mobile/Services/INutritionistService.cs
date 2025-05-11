using NutriPlat.Shared.Dtos;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.Services
{
    public interface INutritionistService
    {
        Task<UserDto?> GetMyNutritionistAsync();
    }
}
