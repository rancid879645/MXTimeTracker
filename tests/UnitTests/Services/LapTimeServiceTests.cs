using Moq;
using MotocrossTracker.API.Application.DTOs;
using MotocrossTracker.API.Application.Interfaces;
using MotocrossTracker.API.Application.Services;
using MotocrossTracker.API.Domain.Entities;
using MotocrossTracker.API.Domain.Interfaces;

namespace MotocrossTracker.UnitTests.Services;

public class LapTimeServiceTests
{
    private readonly Mock<ILapTimeRepository> _repoMock;
    private readonly ILapTimeService _service;

    public LapTimeServiceTests()
    {
        _repoMock = new Mock<ILapTimeRepository>();
        _service = new LapTimeService(_repoMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenExists_ShouldReturnLapTimeDto()
    {
        var lapTime = new LapTime { Id = 1, RiderName = "Jorge Prado", TrackName = "Agueda", Time = TimeSpan.FromSeconds(123), Date = DateTime.UtcNow, UserId = "user-1" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(lapTime);

        var result = await _service.GetByIdAsync(1, "user-1");

        Assert.NotNull(result);
        Assert.Equal("Jorge Prado", result.RiderName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenNotFound_ShouldReturnNull()
    {
        _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((LapTime?)null);

        var result = await _service.GetByIdAsync(99, "user-1");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllByUserAsync_ShouldReturnOnlyUserRecords()
    {
        var lapTimes = new List<LapTime>
        {
            new() { Id = 1, RiderName = "Jorge Prado", TrackName = "Agueda", Time = TimeSpan.FromSeconds(120), Date = DateTime.UtcNow, UserId = "user-1" },
            new() { Id = 2, RiderName = "Tim Gajser", TrackName = "Mantova", Time = TimeSpan.FromSeconds(115), Date = DateTime.UtcNow, UserId = "user-1" }
        };
        _repoMock.Setup(r => r.GetAllByUserAsync("user-1")).ReturnsAsync(lapTimes);

        var result = await _service.GetAllByUserAsync("user-1");

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnNewId()
    {
        var request = new CreateLapTimeRequest { RiderName = "Jorge Prado", TrackName = "Agueda", Time = TimeSpan.FromSeconds(123), Date = DateTime.UtcNow };
        _repoMock.Setup(r => r.CreateAsync(It.IsAny<LapTime>())).ReturnsAsync(42);

        var id = await _service.CreateAsync(request, "user-1");

        Assert.Equal(42, id);
    }

    [Fact]
    public async Task UpdateAsync_WhenRecordBelongsToUser_ShouldReturnTrue()
    {
        var existing = new LapTime { Id = 1, UserId = "user-1" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<LapTime>())).ReturnsAsync(true);

        var request = new UpdateLapTimeRequest { RiderName = "Updated", TrackName = "Mantova", Time = TimeSpan.FromSeconds(100), Date = DateTime.UtcNow };
        var result = await _service.UpdateAsync(1, request, "user-1");

        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenRecordBelongsToDifferentUser_ShouldReturnFalse()
    {
        var existing = new LapTime { Id = 1, UserId = "user-2" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var request = new UpdateLapTimeRequest { RiderName = "Hack", TrackName = "x", Time = TimeSpan.FromSeconds(1), Date = DateTime.UtcNow };
        var result = await _service.UpdateAsync(1, request, "user-1");

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_WhenRecordBelongsToUser_ShouldReturnTrue()
    {
        var existing = new LapTime { Id = 1, UserId = "user-1" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteAsync(1, "user-1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_WhenRecordBelongsToDifferentUser_ShouldReturnFalse()
    {
        var existing = new LapTime { Id = 1, UserId = "user-2" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var result = await _service.DeleteAsync(1, "user-1");

        Assert.False(result);
    }
}
