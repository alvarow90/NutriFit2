using NutriPlat.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.Services
{
    public interface IWorkoutRoutineApiService
    {
        Task<UserWorkoutRoutineDto?> GetMyWorkoutRoutineAsync();

        Task<IEnumerable<UserWorkoutRoutineDto>> GetMyAssignedRoutinesAsync();
    }
}
