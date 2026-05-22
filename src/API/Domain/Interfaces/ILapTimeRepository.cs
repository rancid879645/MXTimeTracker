using MotocrossTracker.API.Domain.Entities;

namespace MotocrossTracker.API.Domain.Interfaces;

public interface ILapTimeRepository
{
    Task<LapTime?> GetByIdAsync(int id);
    Task<IEnumerable<LapTime>> GetAllByUserAsync(string userId);
    Task<int> CreateAsync(LapTime lapTime);
    Task<bool> UpdateAsync(LapTime lapTime);
    Task<bool> DeleteAsync(int id);
}
