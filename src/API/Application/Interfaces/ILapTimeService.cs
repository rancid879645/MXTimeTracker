using MotocrossTracker.API.Application.DTOs;

namespace MotocrossTracker.API.Application.Interfaces;

public interface ILapTimeService
{
    Task<LapTimeDto?> GetByIdAsync(int id, string userId);
    Task<IEnumerable<LapTimeDto>> GetAllByUserAsync(string userId);
    Task<int> CreateAsync(CreateLapTimeRequest request, string userId);
    Task<bool> UpdateAsync(int id, UpdateLapTimeRequest request, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}
