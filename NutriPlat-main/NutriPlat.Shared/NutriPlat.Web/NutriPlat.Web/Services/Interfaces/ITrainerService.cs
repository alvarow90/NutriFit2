// NutriPlat.Web/Services/Interfaces/ITrainerService.cs
using NutriPlat.Shared.Dtos;
using System.Threading.Tasks;

namespace NutriPlat.Web.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<bool> CreateRoutineAsync(WorkoutRoutineDto routine, string jwtToken);
    }
}
