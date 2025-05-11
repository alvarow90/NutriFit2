using NutriPlat.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriPlat.Mobile.Services
{
    public interface IProgressApiService
    {
        Task<IEnumerable<ProgressEntryDto>> GetMyProgressEntriesAsync();
        Task<bool> CreateProgressEntryAsync(ProgressEntryDto entry);
    }
}
