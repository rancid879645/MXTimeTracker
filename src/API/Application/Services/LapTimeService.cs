using MotocrossTracker.API.Application.DTOs;
using MotocrossTracker.API.Application.Interfaces;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Domain.Interfaces;

namespace MotocrossTracker.API.Application.Services;

public class LapTimeService(ILapTimeRepository repository) : ILapTimeService
{
    public async Task<LapTimeDto?> GetByIdAsync(int id, string userId)
    {
        var lapTime = await repository.GetByIdAsync(id);

        if (lapTime is null || lapTime.UserId != userId)
            return null;

        return MapToDto(lapTime);
    }

    public async Task<IEnumerable<LapTimeDto>> GetAllByUserAsync(string userId)
    {
        var lapTimes = await repository.GetAllByUserAsync(userId);
        return lapTimes.Select(MapToDto);
    }

    public async Task<int> CreateAsync(CreateLapTimeRequest request, string userId)
    {
        if (string.IsNullOrWhiteSpace(request.RiderName))
            throw new ArgumentException("Rider name is required.");

        if (string.IsNullOrWhiteSpace(request.TrackName))
            throw new ArgumentException("Track name is required.");

        if (request.Time <= TimeSpan.Zero)
            throw new ArgumentException("Lap time must be greater than zero.");

        var lapTime = new LapTime
        {
            RiderName = request.RiderName,
            TrackName = request.TrackName,
            Time = request.Time,
            Date = request.Date,
            UserId = userId
        };

        return await repository.CreateAsync(lapTime);
    }

    public async Task<bool> UpdateAsync(int id, UpdateLapTimeRequest request, string userId)
    {
        var existing = await repository.GetByIdAsync(id);

        if (existing is null || existing.UserId != userId)
            return false;

        if (string.IsNullOrWhiteSpace(request.RiderName))
            throw new ArgumentException("Rider name is required.");

        if (string.IsNullOrWhiteSpace(request.TrackName))
            throw new ArgumentException("Track name is required.");

        if (request.Time <= TimeSpan.Zero)
            throw new ArgumentException("Lap time must be greater than zero.");

        existing.RiderName = request.RiderName;
        existing.TrackName = request.TrackName;
        existing.Time = request.Time;
        existing.Date = request.Date;

        return await repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var existing = await repository.GetByIdAsync(id);

        if (existing is null || existing.UserId != userId)
            return false;

        return await repository.DeleteAsync(id);
    }

    private static LapTimeDto MapToDto(LapTime lapTime) => new()
    {
        Id = lapTime.Id,
        RiderName = lapTime.RiderName,
        TrackName = lapTime.TrackName,
        Time = lapTime.Time,
        Date = lapTime.Date,
        UserId = lapTime.UserId
    };
}
